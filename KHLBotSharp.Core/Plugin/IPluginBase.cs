using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using System;
using System.Threading.Tasks;

namespace KHLBotSharp
{
    public interface IKHLPlugin<T> : IKHLPlugin where T : AbstractExtra
    {
        /// <summary>
        /// <typeparamref name="T"/>事件处理，插件用
        /// </summary>
        /// <returns></returns>
        Task<bool> Handle(EventMessage<T> eventArgs);

    }

    public interface IKHLPlugin
    {
        /// <summary>
        /// 用于获取已注册的DI Services
        /// </summary>
        /// <returns></returns>
        Task Ctor(IServiceProvider provider);
    }
}
