using KHLBotSharp.Models.EventsMessage.Abstract;
using KHLBotSharp.Models.EventsMessage;

namespace KHLBotSharp.EventHandlers.SystemEvents.Base
{
    public interface ISystemEventHandler<T> : IKHLPlugin<SystemExtra<T>> where T : AbstractBody
    {
    }
}
