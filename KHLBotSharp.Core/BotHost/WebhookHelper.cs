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
            var logService = new LogService();
            logService.Init("Bot", settings);
            service.AddSingleton(typeof(ILogService), logService);
            service.AddSingleton(typeof(IErrorRateService), typeof(ErrorRateService));
            service.AddScoped(typeof(IHttpClientService), typeof(HttpClientService));
            return service;
        }
    }
}
