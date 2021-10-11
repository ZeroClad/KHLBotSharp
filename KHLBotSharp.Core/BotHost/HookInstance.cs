using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;

namespace KHLBotSharp.Core.BotHost
{
    /// <summary>
    /// Webhook专用，区分机器人进程
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class WebHookInstance
    {
        /// <summary>
        /// 机器人进程名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建新的DI，用于区分机器人避免混淆
        /// </summary>
        public IServiceCollection ServiceCollection { get; set; }
        private IServiceProvider _serviceProvider = null;
        /// <summary>
        /// 获取已经Build过的ServiceProvider，用于DI获取所需的Service
        /// </summary>
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
