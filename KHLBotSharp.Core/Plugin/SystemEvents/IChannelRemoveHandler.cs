using KHLBotSharp.EventHandlers.SystemEvents.Base;
using KHLBotSharp.Models.EventsMessage;

namespace KHLBotSharp.EventHandlers.SystemEvents
{
    public interface IChannelRemoveHandler : ISystemEventHandler<ChannelRemovedEvent>
    {
    }
}
