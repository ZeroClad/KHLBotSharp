using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.EventsMessage.Abstract;

namespace KHLBotSharp.EventHandlers.SystemEvents.Base
{
    public interface ISystemEventHandler<T> : IKHLPlugin<SystemExtra<T>> where T : AbstractBody
    {
    }
}
