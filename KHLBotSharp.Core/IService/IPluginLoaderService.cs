using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace KHLBotSharp.IService
{
    public interface IPluginLoaderService
    {
        void Init(IServiceProvider provider);
        void LoadPlugin(string bot, IServiceCollection services);
        IEnumerable<IKHLPlugin> ResolvePlugin();
        IEnumerable<T> ResolvePlugin<T>() where T : IKHLPlugin;
        void HandleMessage<T, T2>(EventMessage<T> input, IEnumerable<T2> plugins) where T : AbstractExtra where T2 : IKHLPlugin<T>;
        void HandleMessage<T>(EventMessage<T> input) where T : AbstractExtra;
    }
}
