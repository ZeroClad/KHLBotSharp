using KHLBotSharp.Common.Request;
using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using KHLBotSharp.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;

namespace KHLBotSharp.Core.BotHost
{
    public static class WebhookHelper
    {
        public static IServiceCollection RegisterKHLBot(this IServiceCollection service)
        {
            var pluginLoader = new PluginLoaderService();
            if (!Directory.Exists("Profiles\\Bot\\Plugins"))
            {
                Directory.CreateDirectory("Profiles\\Bot\\Plugins");
            }
            if (!Directory.Exists("Profiles\\Bot\\Log"))
            {
                Directory.CreateDirectory("Profiles\\Bot\\Log");
            }
            pluginLoader.LoadPlugin("Profiles\\Bot", service);
            service.AddMemoryCache();
            service.AddSingleton(typeof(IPluginLoaderService), pluginLoader);
            service.AddScoped(typeof(IKHLHttpService), typeof(KHLHttpService));
            if (!File.Exists("Profiles\\Bot\\config.json"))
            {
                var config = new BotConfigSettings()
                {
                    EncryptKey = "SampleKey",
                    VerifyToken = "SampleToken"
                };
                File.WriteAllText("Profiles\\Bot\\config.json", JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            var settings = JsonConvert.DeserializeObject<BotConfigSettings>(File.ReadAllText("Profiles\\Bot\\config.json"));
            service.AddSingleton(typeof(IBotConfigSettings), settings);
            var logService = new LogService();
            logService.Init("Bot", settings);
            service.AddSingleton(typeof(ILogService), logService);
            service.AddSingleton(typeof(IErrorRateService), typeof(ErrorRateService));
            service.AddScoped(typeof(IHttpClientService), typeof(HttpClientService));
            return service;
        }
    }
}
