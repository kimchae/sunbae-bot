using Discore;
using Discore.WebSocket;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SunbaeBot.Commands
{
    abstract class Command
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int _args { get; set; } = 0;
        public int _optionalArgs { get; set; } = 0;
        public abstract void Execute(ITextChannel Channel, MessageEventArgs e, MatchCollection args);
    }
}
