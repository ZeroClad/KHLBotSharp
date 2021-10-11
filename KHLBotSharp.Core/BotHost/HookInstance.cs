using Microsoft.Extensions.DependencyInjection;
using System;


namespace KHLBotSharp.Core.BotHost
{
    /// <summary>
    /// Webhook专用，区分机器人进程
    /// </summary>
    public class WebHookInstance
    {
        public string Name { get; set; }
        public IServiceCollection ServiceCollection { get; set; }
        private IServiceProvider _serviceProvider = null;
        public IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    _serviceProvider = ServiceCollection.BuildServiceProvider();
                }
                return _serviceProvider;
            }
        }
    }
}
