using Discore;
using Discore.WebSocket;
using SunbaeBot.Commands;
using SunbaeBot.IO;
using System;
using System.Text.RegularExpressions;

namespace Commands
{
    public class CommandParser
    {
        public static string _prefix { get; private set; } = "!";

        public static void ProcessCommand(object sender, MessageEventArgs e)
        {
            string body = e.Message.Content;
            if (!body.StartsWith(_prefix))
                return;

            Regex pattern = new Regex("([\"'])(?:(?=(\\\\?))\\2.)*?\\1|([^\\s]+)");
            var content = pattern.Matches(body);

            Shard shard = e.Shard;
            DiscordMessage message = e.Message;
            ITextChannel textChannel = (ITextChannel)shard.Cache.Channels.Get(message.ChannelId);
            string cmd = content[0].Value.Substring(1, content[0].Value.Length - 1);
            switch (cmd)
            {
                case "boop":
                    textChannel.SendMessage("test");
                    break;
            }

            if (cmd == "")
            {
                if (content.Count < (0 + 1) - (0))
                {
                    textChannel.SendMessage("Incorrect number of arguments.");
                    return;
                }
                textChannel.SendMessage("teset");
            }
        }

        public static void SetPrefix(string prefix)
        {
            _prefix = prefix;
        }
    }
}
