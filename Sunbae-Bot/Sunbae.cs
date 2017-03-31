using System;
using System.Threading.Tasks;
using SunbaeBot.IO;
using Discore;
using Discore.WebSocket;
using System.Threading;
using Commands;

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

            while (shard.IsRunning)
                await Task.Delay(1000);
        }

        private static async void OnMessageReceived(object sender, MessageEventArgs e)
        {
            Shard shard = e.Shard;
            DiscordMessage message = e.Message;
            ITextChannel textChannel = (ITextChannel)shard.Cache.Channels.Get(message.ChannelId);

            if (message.Author == shard.User)
                return;

            if (message.Content == "!ping")
            {
                try
                {
                    await textChannel.SendMessage($"<@{message.Author.Id}> Pong!");
                }
                catch (Exception) { Log.Error("Error sending message"); }
            }
        }
    }
}