﻿using System;
using System.Collections.Generic;

namespace MAIN
{
	using Card = KeyValuePair<CardColor, string>;

	enum UnoMode : byte
	{
		STACK_D2 = 0x01, // Stack "draw +2" cards
		STACK_WD4 = 0x02, // Stack "wild draw +4" cards
		MULTIPLE = 0x08, // Place same cards (TODO)
		LIGRETTO = 0x80, // Smash in cards whenever you have a matching one
	}

	enum CardColor
	{
		BLUE = IRC_Color.BLUE,
		GREEN = IRC_Color.GREEN,
		RED = IRC_Color.RED,
		YELLOW = IRC_Color.YELLOW,
		NONE = IRC_Color.BLACK
	}

	class UnoPlayer
	{
		public string name;
		public List<Card> cards;

		public UnoPlayer(string name)
		{
			this.name = name;
			cards = new List<Card>();
		}

		public List<Card> DrawCards(int count)
		{
			var drawn = new List<Card>();
			var card_colors = Enum.GetValues(typeof(CardColor));
			var card_faces = m_SuperiorUno.card_faces.ToArray();

			while ((count--) > 0) {
				CardColor color = (CardColor)Utils.RandomIn(card_colors);
				string face = (string)Utils.RandomIn(card_faces);

				if (face.Contains("W"))
					color = CardColor.NONE;
				else if (color == CardColor.NONE)
					color = CardColor.RED; // HACC

				drawn.Add(new Card(color, face));
			}

			cards.AddRange(drawn);
			SortCards();
			return drawn;
		}

		public int GetCardsValue()
		{
			int score = 0;
			foreach (Card card in cards) {
				int value = 10;
				int.TryParse(card.Value, out value);
				score += value;
			}
			return score;
		}

		public void SortCards()
		{
			cards.Sort((a, b) => b.Key.CompareTo(a.Key));
		}
	}

	class UnoChannel
	{
		public List<UnoPlayer> players;
		public string current_player;
		public bool is_active;
		public Card top_card;
		public int draw_count;
		public byte modes { get; private set; }

		public UnoChannel(byte modes)
		{
			players = new List<UnoPlayer>();
			this.modes = modes;
		}

		public bool CheckMode(UnoMode mode)
		{
			return (modes & (byte)mode) != 0;
		}

		public UnoPlayer GetPlayer()
		{
			return GetPlayer(current_player);
		}

		public UnoPlayer GetPlayer(string player)
		{
			return players.Find(item => item.name == player);
		}

		public void TurnNext()
		{
			int index = players.FindIndex(item => item.name == current_player);
			index++; // Also: Error -1 -> 0

			if (index >= players.Count)
				index = 0;

			current_player = players[index].name;
		}
	}

	class m_SuperiorUno : Module
	{
		public static List<string> card_faces = new List<string>
		{ "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "D2", "R", "S", "W", "WD4" };

		Dictionary<string, UnoChannel> m_channels;


		public m_SuperiorUno(Manager manager) : base("SuperiorUno", manager)
		{
			m_channels = new Dictionary<string, UnoChannel>();

			var cmd = manager.GetChatcommand().Add(G.settings["prefix"] + "uno");
			cmd.SetMain(delegate (string nick, string message) {
				var channel = p_manager.GetChannel();
				channel.Say(nick + ": Available subcommands: " + cmd.CommandsToString() +
					". Please check the source code until there's a tutorial.");
			});

			cmd.Add("join", Cmd_Join);
			cmd.Add("leave", Cmd_Leave);
			cmd.Add("deal", Cmd_Deal);
			cmd.Add("p", Cmd_Put);
			cmd.Add("d", Cmd_Draw);
		}

		public override void CleanStage()
		{
			m_channels.Clear();
		}

		public override void OnUserRename(string nick, string old_nick)
		{
			foreach (KeyValuePair<string, UnoChannel> chan in m_channels) {
				UnoPlayer player = chan.Value.players.Find(item => item.name == old_nick);
				if (player != null)
					player.name = nick;

				if (chan.Value.current_player == old_nick)
					chan.Value.current_player = nick;
			}
		}

		public override void OnUserLeave(string nick)
		{
			Channel channel = p_manager.GetChannel();
			UnoChannel uno = GetUnoChannel(channel.GetName());
			if (uno == null)
				return;

			Cmd_Leave(nick, "");
		}

		void Cmd_Join(string nick, string message)
		{
			Channel channel = p_manager.GetChannel();
			UnoChannel uno = GetUnoChannel(channel.GetName());

			if (uno != null && uno.is_active) {
				channel.Say(nick + ": Please wait for " + uno.current_player +
					" to finish their game.");
				return;
			}

			if (uno != null && uno.GetPlayer(nick) != null) {
				E.Notice(nick, "You already joined the game.");
				return;
			}

			// Create a new UnoChannel
			if (uno == null) {
				string modes_s = Chatcommand.GetNext(ref message);

				byte modes = 0;
				try {
					modes = Convert.ToByte(modes_s, 16);
				} catch { }
				uno = new UnoChannel(modes);
				m_channels[channel.GetName()] = uno;
			}

			uno.players.Add(new UnoPlayer(nick));
			uno.current_player = nick;
			channel.Say("[UNO] " + uno.players.Count +
				" player(s) are waiting for a new UNO game. " +
				string.Format("Modes: 0x{0:X2}", uno.modes));
		}

		void Cmd_Leave(string nick, string message)
		{
			Channel channel = p_manager.GetChannel();
			UnoChannel uno = GetUnoChannel(channel.GetName());

			if (uno == null || uno.GetPlayer(nick) == null) {
				E.Notice(nick, "You are not part of an UNO game.");
				return;
			}

			channel.Say(nick + " left this UNO game.");

			if (uno.current_player == nick)
				uno.TurnNext();

			uno.players.RemoveAll(item => item.name == nick);

			bool was_active = uno.is_active;
			if (CheckGameEndDelete(channel.GetName()) && was_active)
				channel.Say("[UNO] Game ended");
		}

		void Cmd_Deal(string nick, string message)
		{
			Channel channel = p_manager.GetChannel();
			UnoChannel uno = GetUnoChannel(channel.GetName());
			UnoPlayer nplayer = uno != null ? uno.GetPlayer(nick) : null;

			if (nplayer == null || uno.is_active) {
				E.Notice(nick, "You are either not part of the game or " +
					" another is already ongoing.");
				return;
			}

			if (uno.players.Count < 2) {
				channel.Say("At least two player are required to start the game.");
				return;
			}

			foreach (UnoPlayer player in uno.players)
				player.DrawCards(9);

			uno.top_card = nplayer.cards[0];
			uno.is_active = true;
			TellGameStatus(channel);
		}

		void Cmd_Put(string nick, string message)
		{
			Channel channel = p_manager.GetChannel();
			UnoChannel uno = GetUnoChannel(channel.GetName());
			UnoPlayer player = uno != null ? uno.GetPlayer(nick) : null;

			if (player == null || !uno.is_active) {
				E.Notice(nick, "huh?? You don't have any cards.");
				return;
			}

			if (uno.current_player != nick && !uno.CheckMode(UnoMode.LIGRETTO)) {
				E.Notice(nick, "It is not your turn (current: " + uno.current_player + ").");
				return;
			}

			string put_color_s = Chatcommand.GetNext(ref message).ToLower();
			CardColor put_color = CardColor.NONE;
			string put_face = Chatcommand.GetNext(ref message).ToUpper();

			// Convert user input to internal format
			switch (put_color_s) {
			case "b": put_color = CardColor.BLUE; break;
			case "r": put_color = CardColor.RED; break;
			case "g": put_color = CardColor.GREEN; break;
			case "y": put_color = CardColor.YELLOW; break;
			}

			bool change_face = false;
			if (put_face.Contains("W")) {
				// Convert/validate W and WD4
				change_face = true;
			}
			if (!change_face && put_color == CardColor.NONE ||
					!card_faces.Contains(put_face)) {

				E.Notice(nick, $"Invalid input. Syntax: {G.settings["prefix"]}uno p <color> <face>.");
				return;
			}

			// Check whether color of face matches
			if (put_color != uno.top_card.Key &&
			    put_face != uno.top_card.Value &&
			    !change_face) {

				E.Notice(nick, "This card cannot be played. Please check color and face.");
				return;
			}

			int card_index = -1;
			if (change_face)
				card_index = player.cards.FindIndex(item => item.Value == put_face);
			else
				card_index = player.cards.FindIndex(
					item => item.Key == put_color && item.Value == put_face);

			if (card_index < 0) {
				E.Notice(nick, "You don't have this card.");
				return;
			}

			if (uno.draw_count > 0) {
				bool ok = false;
				if (put_face == "D2" && uno.CheckMode(UnoMode.STACK_D2))
					ok = true;
				else if (put_face == "WD4" &&
				         put_face == uno.top_card.Value &&
				         uno.CheckMode(UnoMode.STACK_WD4))
					ok = true;

				if (!ok) {
					E.Notice(nick, "You cannot play this card due to the top card.");
					return;
				}
			}

			// All OK. Put the card on top
			uno.top_card = new Card(put_color, put_face);
			player.cards.RemoveAt(card_index);

			bool pending_delete = player.cards.Count == 0;
			bool pending_autodraw = false;

			switch (put_face) {
			case "D2":
				uno.draw_count += 2;
				pending_autodraw = !uno.CheckMode(UnoMode.STACK_D2);
				break;
			case "WD4":
				uno.draw_count += 4;
				pending_autodraw = !uno.CheckMode(UnoMode.STACK_WD4);
				break;
			case "R": uno.players.Reverse(); break;
			case "S": uno.TurnNext(); break;
			}

			uno.TurnNext();

			if (pending_delete) {
				// 'current_player' may not be 'nick'
				uno.players.Remove(player);
			}
			if (pending_delete && uno.players.Count > 0) {
				int score = 0;
				foreach (UnoPlayer up in uno.players)
					score += up.GetCardsValue();

				channel.Say(nick + " finishes the game and gains " + score + " points");
			}

			if (CheckGameEndDelete(channel.GetName()))
				return;

			if (pending_autodraw)
				Cmd_Draw(uno.current_player, "");
			else
				TellGameStatus(channel);
		}

		void Cmd_Draw(string nick, string message)
		{
			Channel channel = p_manager.GetChannel();
			UnoChannel uno = GetUnoChannel(channel.GetName());
			UnoPlayer player = uno != null ? uno.GetPlayer(nick) : null;

			if (player == null || !uno.is_active) {
				E.Notice(nick, "You are not part of an UNO game.");
				return;
			}

			if (uno.current_player != nick && !uno.CheckMode(UnoMode.LIGRETTO)) {
				E.Notice(nick, "It is not your turn (current: " + uno.current_player + ").");
				return;
			}

			var drawn = player.DrawCards(Math.Max(1, uno.draw_count));
			uno.draw_count = 0;
			E.Notice(nick, "You drew following cards: " + FormatCards(drawn));

			uno.TurnNext();
			TellGameStatus(channel);
		}

		UnoChannel GetUnoChannel(string channel)
		{
			return m_channels.ContainsKey(channel) ? m_channels[channel] : null;
		}

		string FormatCards(List<Card> cards)
		{
			var sb = new System.Text.StringBuilder();
			sb.Append((char)0x02); // Bold start
			foreach (Card card in cards) {
				// This sucks. Where's my snprintf?
				sb.Append(Utils.Colorize("[" + card.Value + "] ", (IRC_Color)card.Key, false));
			}
			sb.Append((char)0x0F); // Normal text
			sb.Append(" ");
			return sb.ToString();
		}

		void TellGameStatus(Channel channel)
		{
			UnoChannel uno = GetUnoChannel(channel.GetName());
			if (uno == null)
				return;

			var sb = new System.Text.StringBuilder();
			sb.Append("[UNO] " + uno.current_player);
			sb.Append(" (" + uno.GetPlayer().cards.Count + " cards) - ");
			sb.Append("Top card: " + FormatCards(new List<Card> { uno.top_card }));
			if (uno.draw_count > 0)
				sb.Append("- draw count: " + uno.draw_count);

			channel.Say(sb.ToString());

			var player = uno.GetPlayer();
			E.Notice(player.name, "Your cards: " + FormatCards(player.cards));
		}

		public bool CheckGameEndDelete(string channel)
		{
			UnoChannel uno = GetUnoChannel(channel);
			if (uno == null)
				return true;
			if (uno.players.Count > 1)
				return false;

			m_channels.Remove(channel);
			return true;
		}

	}
}