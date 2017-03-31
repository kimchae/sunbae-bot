using System;
using System.Threading.Tasks;
using SunbaeBot.IO;
using Discore;
using Discore.WebSocket;
using System.Threading;
using SunbaeBot.Commands.Implementation;
using DiscoreCommands;

namespace SunbaeBot
{
    class Sunbae
    {
        public static string version = "1.0";

        static void Main(string[] args) => Start().Wait();

        public static async Task Start()
        {
            Settings.Load();
            Log.Taexify();

            DiscordBotUserToken token = new DiscordBotUserToken(Settings.Sunbae.Token);
            DiscordWebSocketApplication app = new DiscordWebSocketApplication(token);

            Shard shard = app.ShardManager.CreateSingleShard();
            await shard.StartAsync(CancellationToken.None);

            shard.Gateway.OnMessageCreated += CommandParser.ProcessCommand;

            CommandParser.SetPrefix("");
            // Commands
            CommandFactory.RegisterCommand("ping", new PingCommand());

            while (shard.IsRunning)
                await Task.Delay(1000);
        }
    }
}