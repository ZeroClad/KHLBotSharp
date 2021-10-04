using KHLBotSharp.Common.Request;
using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using KHLBotSharp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace KHLBotSharp.Core.BotHost
{
    public static class WebhookHelper
    {
        public static IServiceCollection RegisterKHLBot(this IServiceCollection service)
        {
            if (!Directory.Exists("Profiles"))
            {
                Directory.CreateDirectory("Profiles");
            }
            var publicLog = new LogService();
            publicLog.Init("Public", new BotConfigSettings { Debug = false });
            service.AddSingleton<ILogService>(publicLog);
            return service;
        }
    }
}
