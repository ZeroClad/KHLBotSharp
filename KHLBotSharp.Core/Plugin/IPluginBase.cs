using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using System;
using System.Threading.Tasks;

namespace KHLBotSharp
{
     public interface IKHLPlugin<T>: IKHLPlugin where T : AbstractExtra
    {
        /// <summary>
        /// Handler for <typeparamref name="T"/> Events
        /// </summary>
        /// <returns></returns>
         public Task<bool> Handle(EventMessage<T> eventArgs);

    }

     public interface IKHLPlugin
     {
        /// <summary>
        /// Resolve what you need in DI injection. You can also do loading configs or any preparation things here
        /// </summary>
        /// <returns></returns>
        public Task Ctor(IServiceProvider provider);
     }
}
