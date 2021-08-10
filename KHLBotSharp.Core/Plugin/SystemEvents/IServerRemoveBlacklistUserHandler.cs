using KHLBotSharp.EventHandlers.SystemEvents.Base;
using KHLBotSharp.Models.EventsMessage;

namespace KHLBotSharp.EventHandlers.SystemEvents
{
    public interface IServerRemoveBlacklistUserHandler : ISystemEventHandler<ServerRemoveBlacklistUserEvent>
    {
    }
}
