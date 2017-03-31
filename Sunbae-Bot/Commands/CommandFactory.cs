using System;
using System.Collections.Generic;
using System.Text;

namespace SunbaeBot.Commands
{
    class CommandFactory
    {
        private readonly IDictionary<string, Func<Command>> _commands;

    }
}
