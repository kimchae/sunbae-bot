using System;
using System.Threading.Tasks;
using Sunbae_Bot.IO;
using Discore;
using Discore.WebSocket;
using System.Threading;

namespace Sunbae_Bot
{
    class Sunbae
    {
        public static string version = "1.0";

        static void Main(string[] args) => new Sunbae().Start();

        public async Task Start()
        {
            Log.Taexify();

            DiscordBotUserToken token = new DiscordBotUserToken("");
            DiscordWebSocketApplication app = new DiscordWebSocketApplication(token);

            Shard shard = app.ShardManager.CreateSingleShard();
            await shard.StartAsync(CancellationToken.None);

            shard.Gateway.OnMessageCreated += OnMessageReceived;

            while (shard.IsRunning)
                await Task.Delay(1000);
        }

        private static async void OnMessageReceived(object sender, MessageEventArgs e)
        {
            Shard shard = e.Shard;
            DiscordMessage message = e.Message;

            if (message.Author == shard.User)
                return;

            if (message.Content == "!ping")
            {
                ITextChannel textChannel = (ITextChannel)shard.Cache.Channels.Get(message.ChannelId);
                try
                {
                    await textChannel.SendMessage($"<@{message.Author.Id}> Pong!");
                }
                catch (Exception) { Log.Error("Error sending message"); }
            }
        }
    }
}