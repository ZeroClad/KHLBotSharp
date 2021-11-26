using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using System;
using System.Threading.Tasks;

namespace KHLBotSharp
{
    /// <summary>
    /// 插件interface
    /// </summary>
    /// <typeparam name="T">事件class</typeparam>
    public interface IKHLPlugin<T> : IKHLPlugin where T : Extra
    {
        /// <summary>
        /// <typeparamref name="T"/>事件处理，插件用
        /// </summary>
        /// <returns>true 为事件处理完毕，不需要接下去尝试别的Plugin class</returns>
        Task<bool> Handle(EventMessage<T> eventArgs);
    }

    public interface IKHLTextPlugin<T>: IKHLPlugin<T> where T : Extra
    {
        /// <summary>
        /// 指令名字，可选是否需要
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 其他指令，用于运行相同的功能
        /// </summary>
        string[] Prefix { get; }
        /// <summary>
        /// 指令帮助，用于生成help功能
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 指令分类，用于生成help功能
        /// </summary>
        string Group { get; }
    }

    /// <summary>
    /// 插件interface
    /// </summary>
    public interface IKHLPlugin
    {

    }
}
