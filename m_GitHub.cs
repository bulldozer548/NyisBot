﻿using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

using System.Xml;
using System.Net.Security;
using System.Diagnostics;

namespace MAIN
{
	class m_GitHub
	{
		Dictionary<string, List<string>> github_projects;
		DateTime github_updated;
		Thread github_thread;

		public m_GitHub()
		{
			github_updated = DateTime.Now;
			LoadGithubProjects();
			github_thread = new Thread(NewsFeedThread);
			github_thread.Start();
			E.OnUserSay += OnUserSay;
		}

		void OnUserSay(string nick, ref Channel chan, string message,
			int length, ref string[] args)
		{
			if (args[0] != G.settings["prefix"]+"updghp")
				return;

			if (chan.nicks[nick] != G.settings["owner_hostmask"]) {
				E.Say(chan.name, nick + ": who are you?");
				return;
			}
			LoadGithubProjects();
			E.Say(chan.name, nick + ": Updated! Got in total " + github_projects.Count + " Github projects to check.");
		}

		void LoadGithubProjects()
		{
			github_projects = new Dictionary<string, List<string>>();
			if (!System.IO.File.Exists("github_projects.txt")) {
				L.Log("m_GitHub::LoadGithubProjects, File 'github_projects.txt' not found");
				return;
			}

			string[] lines = System.IO.File.ReadAllLines("github_projects.txt");

			for (int i = 0; i < lines.Length; i++) {
				string cur = lines[i];
				if (cur.Length < 10 || cur.IndexOf('/') == -1)
					continue;

				string[] data = cur.Split(' ', '\t');
				List<string> chans = new List<string>();

				for (int e = 1; e < data.Length; e++) {
					if (data[e].Length < 2 || data[e][0] != '#')
						continue;

					chans.Add(data[e]);
				}
				if (chans.Count > 0) {
					github_projects.Add(data[0], chans);
				} else {
					L.Log("m_GitHub::LoadGithubProjects, " + data[0] + " has no (valid) channels");
				}
			}

			L.Log("m_GitHub::LoadGithubProjects, entries = " + github_projects.Count);
		}

		void NewsFeedThread()
		{
			L.Log("m_GitHub::NewsFeedThread start");
			//github_updated -= new TimeSpan(2, 0, 0);
			E.running = true;
			while (E.running) {
				Thread.Sleep(2000);
				foreach (KeyValuePair<string, List<string>> repo in github_projects) {
					if (!E.running)
						return;

					try {
						GetGithubCommits(repo);
					} catch (Exception ex) {
						L.Dump("m_GitHub::NewsFeedThread", repo.Key, ex.ToString());
					}
				}
				L.Log("m_GitHub::NewsFeedThread check completed");
				github_updated = DateTime.Now;
				for (int i = 0; i < 60 * 5; i++) {
					if (!E.running)
						return;

					Thread.Sleep(1000);
				}
			}
			L.Log("m_GitHub::NewsFeedThread stop");
		}

		void GetGithubCommits(KeyValuePair<string, List<string>> repo)
		{
			string branch = "master";
			string[] repo_info = repo.Key.Split(':');
			if (repo_info.Length >= 2)
				branch = repo_info[1];

			#region Download
			string url = "/" + repo_info[0] + "/commits/" + branch + ".atom";
			string content = RequestResponse(url);

			if (content == null)
				return;
			#endregion

			XmlDocument docwest = new XmlDocument();
			try {
				docwest.LoadXml(content);
			} catch(Exception e) {
				L.Dump("m_GitHub::GetGithubCommits", url, e.Message + "\n" + e.ToString());
				return;
			}

			int count = 0;

			foreach (XmlNode node in docwest.GetElementsByTagName("entry")) {
				DateTime timestamp = Convert.ToDateTime(node["updated"].InnerText); // Including my timezone!
				if (timestamp < github_updated || count >= 2)
					break;

				string cappucino = node["id"].InnerText.Split('/')[1].Remove(6);
				string budspencer = node["title"].InnerText.Trim();
				string terencehill = node["author"]["name"].InnerText;
				if (budspencer.Length > 160)
					budspencer = budspencer.Remove(150) + " ...";

				string chucknorris = E.colorize(terencehill, 3)
					+ " @" + E.colorize(repo.Key.Split('/')[1], 5)
					+ ": " + E.colorize(budspencer, 14)
					+ " -> https://github.com/" + repo_info[0] + "/commit/" + cappucino;

				if (repo.Value == null) {
					for (int x = 0; x < E.chans.Length; x++)
						if (E.chans[x] != null && E.chans[x].name[0] == '#') {
							Thread.Sleep(200);
							E.Say(E.chans[x].name, chucknorris);
						}
				} else {
					foreach (string chan in repo.Value) {
						Thread.Sleep(200);
						E.Say(chan, chucknorris);
					}
				}
				count++;
			}
		}

		string RequestResponse(string address)
		{
			ProcessStartInfo info = new ProcessStartInfo();
			info.FileName = "curl";
			info.Arguments = "--http1.1 --url https://github.com" + address;
			info.UseShellExecute = false;
			info.RedirectStandardOutput = true;
			info.RedirectStandardError = true;
			Process curl = Process.Start(info);
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			string last_content = "";
			do {
				last_content = curl.StandardOutput.ReadLine();
				sb.AppendLine(last_content);
				// Wait for </feed> or similar for error pages
			} while (!last_content.StartsWith("</"));

			curl.Close();

			string content = sb.ToString();
			sb = null;

			// Find start of actual XML data
			int content_start = content.IndexOf("<?xml", 0, Math.Min(content.Length, 512));
			int content_end = content.LastIndexOf("</feed>", content.Length - 1, 128);
			if (content_start < 0 || content_end < 0) {
				L.Log("m_GitHub::RequestResponse failed! " + address
					+ ", content length: " + content.Length);
				return null;
			}
			return content;
		}
	}
}