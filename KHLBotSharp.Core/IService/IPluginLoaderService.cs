using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KHLBotSharp.IService
{
    /// <summary>
    /// 内部使用，无需搞懂
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IPluginLoaderService
    {
        void Init(IServiceProvider provider);
        void LoadPlugin(string bot, IServiceCollection services);
        IEnumerable<IKHLPlugin> ResolvePlugin();
        IEnumerable<T> ResolvePlugin<T>() where T : IKHLPlugin;
        void HandleMessage<T, T2>(EventMessage<T> input, IEnumerable<T2> plugins) where T : Extra where T2 : IKHLPlugin<T>;
        void HandleMessage<T>(EventMessage<T> input) where T : Extra;
        bool Inited { get; }
    }
}
