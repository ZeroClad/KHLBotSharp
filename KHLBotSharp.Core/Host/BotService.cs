using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using KHLBotSharp.Common.Request;
using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.EventsMessage.Body;
using KHLBotSharp.Models.EventsMessage.Extra;
using KHLBotSharp.Models.EventsMessage.Text;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.Objects;
using KHLBotSharp.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace KHLBotSharp.Host
{
    public class BotService
    {
        private ClientWebSocket ws;
        private HttpClient hc;
        private IServiceProvider provider;
        public User me { get; private set; }
        public ILogService logService { get; private set; }
        private long sn;
        private Timer timeoutTimer;
        private CancellationTokenSource reset = new CancellationTokenSource();
        private IPluginLoaderService pluginLoader;
        private Timer timer = new Timer();
        private BotConfigSettings settings;
        public BotService(string bot)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(ILogService), typeof(LogService));
            serviceCollection.AddSingleton(typeof(IPluginLoaderService), typeof(PluginLoaderService));
            serviceCollection.AddSingleton(typeof(IHttpClientService), typeof(HttpClientService));
            serviceCollection.AddScoped(typeof(IKHLHttpService), typeof(KHLHttpService));
            ws = new ClientWebSocket();
            hc = new HttpClient();
            pluginLoader = new PluginLoaderService();
            pluginLoader.LoadPlugin(bot, serviceCollection);
            if (!File.Exists(Path.Combine(bot, "config.json")))
            {
                File.WriteAllText(Path.Combine(bot, "config.json"), JsonConvert.SerializeObject(new BotConfigSettings(), Formatting.Indented));
            }
            settings = JsonConvert.DeserializeObject<BotConfigSettings>(File.ReadAllText(Path.Combine(bot, "config.json")));
            serviceCollection.AddSingleton(typeof(IBotConfigSettings), settings);
            provider = serviceCollection.BuildServiceProvider();
            hc.BaseAddress = new Uri("https://www.kaiheila.cn/api/v" + settings.APIVersion + "/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", settings.BotToken);
            provider.GetService<IHttpClientService>().Init(hc);
            logService = provider.GetService<ILogService>();
#pragma warning disable CS0612
            logService.Init(bot.Split('\\').Last());
#pragma warning restore CS0612
            logService.Info("Completed init bot");
        }

        public async Task Run()
        {
            if (!settings.Active)
            {
                logService.Warning("Bot is done loaded but disabled to run. Please set inside your config.json to reactive it");
                return;
            }
            try
            {
                var response = await hc.GetAsync("user/me");
                var json = await response.Content.ReadAsStringAsync();
                me = JsonConvert.DeserializeObject<KHLResponseMessage<User>>(json).Data;
                logService.Info("Bot ID readed as " + me.Id + " Name: " + me.Nick);
                response = await hc.GetAsync("gateway/index");
                json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<JObject>(json);
                var socketUrl = result["data"]["url"].ToString();
                await ws.ConnectAsync(new Uri(socketUrl), CancellationToken.None);
                logService.Info("WebSocket Established");
                _ = Task.Run(async () =>
                {
                    do
                    {
                        logService.Info("WebSocket Ready Listening for events");
                        while (ws.State == WebSocketState.Open && !reset.IsCancellationRequested)
                        {
                            try
                            {
                                ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[8192]);
                                WebSocketReceiveResult wsResult = null;
                                using (var ms = new MemoryStream())
                                {
                                    do
                                    {
                                        wsResult = await ws.ReceiveAsync(buffer, reset.Token);
                                        ms.Write(buffer.Array, buffer.Offset, wsResult.Count);
                                    }
                                    while (!wsResult.EndOfMessage);

                                    ms.Seek(0, SeekOrigin.Begin);

                                    if (wsResult.MessageType == WebSocketMessageType.Binary)
                                    {
                                        int decompressedLength = 0;
                                        byte[] decompressedData = new byte[8192];
                                        using (InflaterInputStream inflater = new InflaterInputStream(ms))
                                            decompressedLength = inflater.Read(decompressedData, 0, decompressedData.Length);
                                        json = Encoding.UTF8.GetString(decompressedData, 0, decompressedLength);
                                        var eventMsg = JObject.Parse(json);
                                        switch (eventMsg["s"].ToString())
                                        {
                                            case "0":
                                                logService.Debug("Received Event, Triggering Plugins");
                                                var channelType = eventMsg.Value<JToken>("d").Value<string>("channel_type");
                                                if (channelType == "GROUP")
                                                {
                                                    switch (eventMsg.Value<JToken>("d").Value<int>("type"))
                                                    {
                                                        case 1:
                                                            var groupText = eventMsg.ToObject<EventMessage<GroupTextMessageEvent>>();
                                                            if (!groupText.Data.Extra.Author.IsBot)
                                                            {
                                                                pluginLoader.HandleMessage(groupText, provider);
                                                            }
                                                            break;
                                                        case 2:
                                                            pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<GroupPictureMessageEvent>>(), provider);
                                                            break;
                                                        case 3:
                                                            pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<GroupVideoMessageEvent>>(), provider);
                                                            break;
                                                        case 9:
                                                            pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<GroupKMarkdownMessageEvent>>(), provider);
                                                            break;
                                                        case 10:
                                                            pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<GroupCardMessageEvent>>(), provider);
                                                            break;
                                                        case 255:
                                                            var extra = eventMsg.Value<JToken>("d").Value<JToken>("extra").Value<string>("type");
                                                            switch (extra)
                                                            {
                                                                case "added_reaction":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelUserAddReactionEvent>>>(), provider);
                                                                    break;
                                                                case "deleted_reaction":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelUserRemoveReactionEvent>>>(), provider);
                                                                    break;
                                                                case "updated_message":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelMessageUpdateEvent>>>(), provider);
                                                                    break;
                                                                case "deleted_message":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelMessageRemoveEvent>>>(), provider);
                                                                    break;
                                                                case "added_channel":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelCreatedEvent>>>(), provider);
                                                                    break;
                                                                case "updated_channel":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelModifyEvent>>>(), provider);
                                                                    break;
                                                                case "deleted_channel":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelRemovedEvent>>>(), provider);
                                                                    break;
                                                                case "pinned_message":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelPinnedMessageEvent>>>(), provider);
                                                                    break;
                                                                case "unpinned_message":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ChannelRemovePinMessageEvent>>>(), provider);
                                                                    break;
                                                                case "joined_guild":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerNewMemberJoinEvent>>>(), provider);
                                                                    break;
                                                                case "exited_guild":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerMemberExitEvent>>>(), provider);
                                                                    break;
                                                                case "updated_guild_member":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerMemberModifiedEvent>>>(), provider);
                                                                    break;
                                                                case "guild_member_online":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerMemberOnlineEvent>>>(), provider);
                                                                    break;
                                                                case "guild_member_offline":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerMemberOfflineEvent>>>(), provider);
                                                                    break;
                                                                case "added_role":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRoleAddEvent>>>(), provider);
                                                                    break;
                                                                case "deleted_role":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRoleRemoveEvent>>>(), provider);
                                                                    break;
                                                                case "updated_role":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRoleModifyEvent>>>(), provider);
                                                                    break;
                                                                case "updated_guild":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerUpdateEvent>>>(), provider);
                                                                    break;
                                                                case "deleted_guild":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRemoveEvent>>>(), provider);
                                                                    break;
                                                                case "added_block_list":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerBlacklistUserEvent>>>(), provider);
                                                                    break;
                                                                case "deleted_block_list":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<ServerRemoveBlacklistUserEvent>>>(), provider);
                                                                    break;
                                                                case "joined_channel":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<UserJoinVoiceChannelEvent>>>(), provider);
                                                                    break;
                                                                case "exited_channel":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<UserExitVoiceChannelEvent>>>(), provider);
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                }
                                                else if (channelType == "BROADCAST")
                                                {
                                                    logService.Info("Received system message broadcast. Skipping data process");
                                                    break;
                                                }
                                                else
                                                {
                                                    switch (eventMsg.Value<JToken>("d").Value<int>("type"))
                                                    {
                                                        case 1:
                                                            pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivateTextMessageEvent>>(), provider);
                                                            break;
                                                        case 2:
                                                            pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivatePictureMessageEvent>>(), provider);
                                                            break;
                                                        case 3:
                                                            pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivateVideoMessageEvent>>(), provider);
                                                            break;
                                                        case 9:
                                                            pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivateKMarkdownMessageEvent>>(), provider);
                                                            break;
                                                        case 10:
                                                            pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<PrivateCardMessageEvent>>(), provider);
                                                            break;
                                                        case 255:
                                                            var extra = eventMsg.Value<JToken>("d").Value<JToken>("extra").Value<string>("type");
                                                            switch (extra)
                                                            {
                                                                case "updated_private_message":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<PrivateMessageModifyEvent>>>(), provider);
                                                                    break;
                                                                case "deleted_private_message":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<PrivateMessageRemoveEvent>>>(), provider);
                                                                    break;
                                                                case "private_added_reaction":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<PrivateMessageAddReactionEvent>>>(), provider);
                                                                    break;
                                                                case "private_deleted_reaction":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<PrivateMessageRemoveReactionEvent>>>(), provider);
                                                                    break;
                                                                case "user_updated":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<UserInfoChangeEvent>>>(), provider);
                                                                    break;
                                                                case "message_btn_click":
                                                                    pluginLoader.HandleMessage(eventMsg.ToObject<EventMessage<SystemExtra<CardMessageButtonClickEvent>>>(), provider);
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                }
                                                sn = eventMsg.Value<long>("sn");
                                                break;
                                            case "1":
                                                logService.Info("WebSocket Handshake Success");
                                                timer.Interval = 30000;
                                                timer.Elapsed += Timer_Elapsed;
                                                timer.Start();
                                                break;
                                            case "3":
                                                logService.Debug("Received Pong");
                                                if (timeoutTimer != null)
                                                {
                                                    timeoutTimer.Stop();
                                                }
                                                break;
                                        }

                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                logService.Error(e.Message + e.StackTrace);
                                break;
                            }
                        }
                        try
                        {
                            timer.Stop();
                            timeoutTimer.Stop();
                            reset.Cancel();
                            reset.Dispose();
                            reset = new CancellationTokenSource();
                            logService.Error("WebSocket Disconnected");
                            try
                            {
                                //Try close again websocket first
                                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                            }
                            catch
                            {

                            }
                            finally
                            {
                                //Wait 3 sec
                                await Task.Delay(3000);
                            }
                            ws.Dispose();
                            response = await hc.GetAsync("gateway/index");
                            json = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<JObject>(json);
                            socketUrl = result["data"]["url"].ToString();
                            ws = new ClientWebSocket();
                            await ws.ConnectAsync(new Uri(socketUrl), CancellationToken.None);
                        }
                        catch (Exception ex)
                        {
                            logService.Error(ex.Message + ex.StackTrace);
                        }

                    }
                    while (true);
                });
            }
            catch (Exception ex)
            {
                logService.Error(ex.ToString());
            }


        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            logService.Debug("Sending Ping");
            _ = ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JObject.FromObject(new { s = 2, sn = sn }).ToString())), WebSocketMessageType.Text, true, CancellationToken.None);
            if (timeoutTimer == null)
            {
                timeoutTimer = new Timer();
                timeoutTimer.Interval = 6000;
                timeoutTimer.Elapsed += TimeoutTimer_Elapsed;
            }
            timeoutTimer.Start();
        }

        private void TimeoutTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            logService.Error("Websocket Timeout");
            reset.Cancel();
            timeoutTimer.Stop();
            timer.Stop();
        }
    }
}
