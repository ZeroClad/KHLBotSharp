using KHLBotSharp.Common.Request;
using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using KHLBotSharp.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KHLBotSharp.Core.BotHost
{
    public static class WebhookHelper
    {
        public static IServiceCollection RegisterKHLBot(this IServiceCollection service)
        {
            var pluginLoader = new PluginLoaderService();
            if (!Directory.Exists("Plugins"))
            {
                Directory.CreateDirectory("Plugins");
            }
            pluginLoader.LoadPlugin(Environment.CurrentDirectory, service);
            service.AddSingleton(typeof(IPluginLoaderService), pluginLoader);
            service.AddScoped(typeof(IKHLHttpService), typeof(KHLHttpService));
            if (!File.Exists("config.json"))
            {
                File.WriteAllText("config.json", JsonConvert.SerializeObject(new BotConfigSettings(), Formatting.Indented));
            }
            var settings = JsonConvert.DeserializeObject<BotConfigSettings>(File.ReadAllText("config.json"));
            service.AddSingleton(typeof(IBotConfigSettings), settings);

            var hc = new HttpClient();
            hc.Timeout = new TimeSpan(0, 0, 30);
            hc.BaseAddress = new Uri("https://www.kaiheila.cn/api/v" + settings.APIVersion + "/");
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", settings.BotToken);
            var logService = new LogService();
            logService.Init("Bot", settings);
            service.AddSingleton(typeof(ILogService), logService);
            var errorRate = new ErrorRateService(logService);
            service.AddSingleton(typeof(IErrorRateService), typeof(ErrorRateService));
            var httpservice = new HttpClientService(logService, errorRate);
#pragma warning disable CS0618 // Type or member is obsolete
            httpservice.Init(hc);
#pragma warning restore CS0618 // Type or member is obsolete
            service.AddSingleton(typeof(IHttpClientService), httpservice);
            return service;
        }
    }
}
