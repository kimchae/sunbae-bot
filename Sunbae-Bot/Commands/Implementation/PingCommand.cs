﻿using Discore;
using Discore.WebSocket;
using System.Text.RegularExpressions;
using DiscoreCommands;

namespace SunbaeBot.Commands.Implementation
{
    class PingCommand : Command
    {
        public PingCommand() : base()
        {
            Name = "ping";
            Desc = "Check if bot is alive.";
        }

        public override async void Execute(ITextChannel Channel, MessageEventArgs e, MatchCollection args)
        {
            await Channel.CreateMessage("pong");
        }
    }
}
