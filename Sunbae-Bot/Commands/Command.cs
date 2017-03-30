using Discore.WebSocket;
using SunbaeBot.IO;
using System;
using System.Text.RegularExpressions;

namespace Commands
{
    public class Command
    {
        public static string _prefix { get; private set; } = "!";

        public static void ProcessCommand(object sender, MessageEventArgs e)
        {
            string body = e.Message.Content;
            if (!body.StartsWith(_prefix))
                return;

            Regex pattern = new Regex("([\"'])(?:(?=(\\\\?))\\2.)*?\\1|([^\\s]+)");
            var content = pattern.Matches(body);

            foreach (Group group in content)
            {
                Log.Inform(group.Value);
            }
        }

        public static void SetPrefix(string prefix)
        {
            _prefix = prefix;
        }
    }
}
