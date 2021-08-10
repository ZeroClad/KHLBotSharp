using KHLBotSharp.EventHandlers.SystemEvents.Base;
using KHLBotSharp.Models.EventsMessage;

namespace KHLBotSharp.EventHandlers.SystemEvents
{
    public interface IServerBlacklistUserHandler : ISystemEventHandler<ServerBlacklistUserEvent>
    {
    }
}
