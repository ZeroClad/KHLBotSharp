using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using KHLBotSharp.Common.MessageParser;
using KHLBotSharp.Common.Request;
using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using KHLBotSharp.Models.Objects;
using KHLBotSharp.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spectre.Console;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace KHLBotSharp.Core.BotHost
{
    /// <summary>
    /// WebSocket专用，区分机器人进程
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class BotService
    {
        private ClientWebSocket ws;
        private readonly IServiceProvider provider;
        public User Me { get; private set; }
        public ILogService LogService { get; private set; }
        private long sn;
        private Timer timeoutTimer;
        private CancellationTokenSource reset = new CancellationTokenSource();
        private readonly IPluginLoaderService pluginLoader;
        private readonly Timer timer = new Timer();
        private readonly Timer errorRate = new Timer();
        private readonly BotConfigSettings settings;
        private readonly IHttpClientService hc;
        private Stopwatch pingTime;
        private readonly IMemoryCache Cache;
        public BotService(string bot)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(ILogService), typeof(LogService));
            serviceCollection.AddScoped(typeof(IHttpClientService), typeof(HttpClientService));
            serviceCollection.AddScoped(typeof(IKHLHttpService), typeof(KHLHttpService));
            serviceCollection.AddSingleton(typeof(IErrorRateService), typeof(ErrorRateService));
            serviceCollection.AddMemoryCache();
            ws = new ClientWebSocket();
            pluginLoader = new PluginLoaderService();
            pluginLoader.LoadPlugin(bot, serviceCollection);
            if (!File.Exists(Path.Combine(bot, "config.json")))
            {
                File.WriteAllText(Path.Combine(bot, "config.json"), JsonConvert.SerializeObject(new BotConfigSettings(), Formatting.Indented));
            }
            settings = new BotConfigSettings();
            settings.Load(bot);
            if (string.IsNullOrEmpty(settings.BotToken))
            {
                settings.BotToken = AnsiConsole.Ask<string>("Input BotToken");
                settings.Save();
            }
            serviceCollection.AddSingleton(typeof(IBotConfigSettings), settings);
            provider = serviceCollection.BuildServiceProvider();
            pluginLoader.Init(provider);
            LogService = provider.GetService<ILogService>();
            hc = provider.GetService<IHttpClientService>();
            Cache = provider.GetService<IMemoryCache>();
            timer.Interval = 30000;
            timer.Elapsed += Timer_Elapsed;
            //Yeah we just don't want any plugin programmer call this so thats why
#pragma warning disable CS0612
            LogService.Init(bot.Split('\\').Last(), settings);
#pragma warning restore CS0612
            if (settings.Debug)
            {
                LogService.Warning("Debug activated, this will cause more logs appears!");
            }
            LogService.Info("Completed init bot");
        }
        /// <summary>
        /// 运行机器人
        /// </summary>
        /// <param name="atMe"></param>
        /// <returns></returns>
        public async Task Run(bool? atMe = null)
        {
            if (!settings.Active)
            {
                LogService.Warning("Bot is done loaded but disabled to run. Please set inside your config.json to reactive it");
                return;
            }
            var error = provider.GetService<IErrorRateService>();
            errorRate.Elapsed += (args, obj) =>
            {
                error.ReportStatus();
                error.CheckResetError();
            };
            errorRate.Interval = 5000;
            errorRate.Start();
            if (atMe != null)
            {
                settings.AtMe = atMe.Value;
            }
            if (!settings.AtMe)
            {
                LogService.Warning("Bot don't need @ to trigger plugins on chat, it won't fulfill KHL Public Bot Request!");
            }
            if (!(settings.ProcessChar.Contains(".") && settings.ProcessChar.Contains("。")))
            {
                LogService.Warning("Bot isn't using command '.' or '。'to trigger plugins on chat, it won't fulfill KHL Public Bot Request!");
            }
            if (settings.BotToken == null)
            {
                LogService.Error("Bot Token not found! Please set inside your config.json to let it works! Bot stopped!");
                return;
            }
            //If we get error on here, we keep loops
            do
            {
                try
                {
                    Me = (await hc.GetAsync<KHLResponseMessage<User>>("user/me", HttpCompletionOption.ResponseHeadersRead)).Data;
                    LogService.Info("Bot ID readed as " + Me.Id + " Name: " + Me.Nick);
                    var result = await hc.GetAsync<JObject>("gateway/index", HttpCompletionOption.ResponseHeadersRead);
                    var socketUrl = result["data"]["url"].ToString();
                    await ws.ConnectAsync(new Uri(socketUrl), CancellationToken.None);
                    LogService.Info("WebSocket Established");
                    _ = Task.Run(async () =>
                    {
                        do
                        {
                            LogService.Info("WebSocket Ready Listening for events");
                            await ListenSocket();
                            //We should infinite retry connect socket whatever happens
                            await RestartSocket();
                        }
                        while (true);
                    });
                    break;
                }
                catch (Exception ex)
                {
                    LogService.Error(ex.Message);
                    error.AddError();
                }
            }
            while (true);
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LogService.Debug("Sending Ping");
            _ = ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JObject.FromObject(new { s = 2, sn }).ToString())), WebSocketMessageType.Text, true, CancellationToken.None);
            if (timeoutTimer == null)
            {
                timeoutTimer = new Timer
                {
                    Interval = 6000
                };
                timeoutTimer.Elapsed += TimeoutTimer_Elapsed;
            }
            if (pingTime == null)
            {
                pingTime = new Stopwatch();
            }
            pingTime.Restart();
            timeoutTimer.Start();
        }

        private void TimeoutTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LogService.Error("Websocket Timeout");
            reset.Cancel();
            timeoutTimer.Stop();
            timer.Stop();
        }

        private async Task ListenSocket()
        {
            while (ws.State == WebSocketState.Open && !reset.IsCancellationRequested)
            {
                try
                {
                    ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[8192]);
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
                            await ParseEvent(ms);
                        }
                    }
                }
                catch (Exception e)
                {
                    //Plugin error
                    LogService.Error(e.Message + e.StackTrace);
                    break;
                }
            }
        }

        private async Task ParseEvent(MemoryStream ms)
        {
            Stopwatch speedTest = Stopwatch.StartNew();
            int decompressedLength = 0;
            byte[] decompressedData = new byte[40960];
            using (InflaterInputStream inflater = new InflaterInputStream(ms))
            {
                decompressedLength = inflater.Read(decompressedData, 0, decompressedData.Length);
            }
            var json = Encoding.UTF8.GetString(decompressedData, 0, decompressedLength);
            var eventMsg = JObject.Parse(json);
            if (sn == eventMsg.Value<long>("sn"))
            {
                speedTest.Stop();
                LogService.Debug("Message Processed in " + speedTest.ElapsedMilliseconds + " ms");
            }
            await eventMsg.ParseEvent(pluginLoader, settings, LogService, Cache, hc);
            speedTest.Stop();
            LogService.Debug("Message Processed in " + speedTest.ElapsedMilliseconds + " ms");
        }

        private async Task RestartSocket()
        {
            try
            {
                timer.Stop();
                if (timeoutTimer != null)
                {
                    timeoutTimer.Stop();
                }
                reset.Cancel();
                reset.Dispose();
                reset = new CancellationTokenSource();
                LogService.Error("WebSocket Disconnected");
                try
                {
                    //Try close again websocket first
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                    ws.Dispose();
                }
                catch
                {

                }
                finally
                {
                    //Wait 3 sec
                    await Task.Delay(3000);
                }
            }
            catch (Exception ex)
            {
                //Socket restart failed
                LogService.Error(ex.Message + ex.StackTrace);
            }
            try
            {
                var result = await hc.GetAsync<JObject>("gateway/index");
                var socketUrl = result["data"]["url"].ToString();
                ws = new ClientWebSocket();
                await ws.ConnectAsync(new Uri(socketUrl), CancellationToken.None);
            }
            catch (Exception ex)
            {
                //Socket restart failed
                LogService.Error(ex.Message + ex.StackTrace);
            }
        }
    }
}
