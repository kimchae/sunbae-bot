using System;
using System.Threading.Tasks;
using SunbaeBot.IO;
using Discore;
using Discore.WebSocket;
using System.Threading;
using SunbaeBot.Commands.Implementation;
using DiscoreCommands;
using Discore.Http;
using System.Linq;

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
            shard.Gateway.OnMessageCreated += ProcessMessage;

            CommandParser.SetPrefix(".");

            // Commands
            CommandFactory.RegisterCommand("ping", new PingCommand());

            while (shard.IsRunning)
                await Task.Delay(1000);
        }

        private static void ProcessMessage(object sender, MessageEventArgs e)
        {
            if (e.Shard.User.Id == e.Message.Author.Id)
                return;

            ITextChannel Channel = (ITextChannel)e.Shard.Cache.Channels.Get(e.Message.ChannelId);
            DiscordMessage message = e.Message;
            DiscoreCache cache = e.Shard.Cache;
            DiscordGuildTextChannel guildTextChannel = cache.Channels.Get(message.ChannelId) as DiscordGuildTextChannel;
            DiscoreGuildCache guildCache = cache.Guilds.Get(guildTextChannel.GuildId);
            DiscordGuildMember member = guildCache.Members.Get(e.Message.Author.Id).Value;

            if (Channel.Id == Snowflake.Parse(Settings.Sunbae.ChannelID))
            {
                String[] bannedRoles = new String[] { "Encoders", "Uploaders", "Sunbae", "admins" };

                foreach (DiscordRole role in guildCache.Roles.Values)
                {
                    if (bannedRoles.Contains(role.Name))
                        continue;

                    if (role.Name.ToLower() == e.Message.Content.ToLower())
                    {
                        member.AddRole(role.Id);
                        break;
                    }
                }
                e.Message.Delete();
            }
        }
    }
}