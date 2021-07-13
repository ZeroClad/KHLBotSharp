using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace KHLBotSharp.IService
{
    public interface IPluginLoaderService
    {
        public void LoadPlugin(string bot, IServiceCollection services);
        public IEnumerable<IKHLPlugin> ResolvePlugin(IServiceProvider provider);
        public IEnumerable<T> ResolvePlugin<T>(IServiceProvider provider) where T : IKHLPlugin;
        public void HandleMessage<T, T2>(EventMessage<T> input, IEnumerable<T2>  plugins) where T : AbstractExtra where T2 : IKHLPlugin<T>;
        public void HandleMessage<T>(EventMessage<T> input, IServiceProvider provider) where T : AbstractExtra;
    }
}
