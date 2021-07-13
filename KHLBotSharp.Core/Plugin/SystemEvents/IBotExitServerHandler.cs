using KHLBotSharp.EventHandlers.SystemEvents.Base;
using KHLBotSharp.Models.EventsMessage.Body;

namespace KHLBotSharp.EventHandlers.SystemEvents
{
    public interface IBotExitServerHandler: ISystemEventHandler<BotExitServerEvent>
    {
    }
}
