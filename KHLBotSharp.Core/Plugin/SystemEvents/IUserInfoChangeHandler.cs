using KHLBotSharp.EventHandlers.SystemEvents.Base;
using KHLBotSharp.Models.EventsMessage;

namespace KHLBotSharp.EventHandlers.SystemEvents
{
    public interface IUserInfoChangeHandler : ISystemEventHandler<UserInfoChangeEvent>
    {
    }
}
