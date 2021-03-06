﻿using System;
namespace MAIN
{
	public class m_Builtin : Module
	{
		public m_Builtin(Manager manager) : base("Builtin", manager)
		{
			var cmd = p_manager.GetChatcommand();
			cmd.Add(G.settings["prefix"] + "help", Cmd_help);
			cmd.Add(G.settings["prefix"] + "c", Cmd_c);
		}

		public override void OnUserSay(string nick, string message,
				int length, ref string[] args)
		{
			Channel channel = p_manager.GetChannel();
			string hostmask = channel.GetUserData(nick).hostmask;

			switch (args[0].Substring(G.settings["prefix"].Length)) {
			case "join":
				if (hostmask != G.settings["owner_hostmask"]) {
					channel.Say(nick + ": who are you?");
					return;
				}
				if (args[1] != "")
					E.Join(args[1]);
				break;
			case "part":
				if (hostmask != G.settings["owner_hostmask"]) {
					channel.Say(nick + ": who are you?");
					return;
				}
				if (args[1] != "")
					E.Part(args[1]);
				break;
			}
		}

		void Cmd_help(string nick, string message)
		{
			var channel = p_manager.GetChannel();
			var cmd = p_manager.GetChatcommand();

			channel.Say(nick + $": [{G.settings["prefix"]}lua/{G.settings["prefix"]}] <text../help()>, {G.settings["prefix"]}c <text..>, " +
				cmd.CommandsToString() + ". See also: " +
				"https://github.com/SmallJoker/NyisBot/blob/master/HELP.txt");
		}

		void Cmd_c(string nick, string message)
		{
			var channel = p_manager.GetChannel();

			var colorized = new System.Text.StringBuilder(message.Length * 7);
			for (int i = 0; i < message.Length; i++) {
				colorized.Append((char)3);
				if ((i & 1) == 0)
					colorized.Append("04,09");
				else
					colorized.Append("09,04");
				colorized.Append(message[i]);
			}

			channel.Say(nick + ": " + colorized.ToString());
		}
	}
}
