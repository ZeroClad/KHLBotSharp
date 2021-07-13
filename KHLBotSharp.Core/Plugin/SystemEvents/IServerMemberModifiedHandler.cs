﻿using KHLBotSharp.EventHandlers.SystemEvents.Base;
using KHLBotSharp.Models.EventsMessage.Body;

namespace KHLBotSharp.EventHandlers.SystemEvents
{
    public interface IServerMemberModifiedHandler:ISystemEventHandler<ServerMemberModifiedEvent>
    {
    }
}
