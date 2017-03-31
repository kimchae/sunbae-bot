using Discore;
using System.Text.RegularExpressions;

namespace SunbaeBot.Commands
{
    abstract class Command
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int _args { get; set; } = 0;
        public int _optionalArgs { get; set; } = 0;

        public Command(MatchCollection args)
        {
            
        }
    }
}
