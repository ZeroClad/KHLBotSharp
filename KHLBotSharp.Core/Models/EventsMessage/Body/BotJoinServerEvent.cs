﻿using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class BotJoinServerEvent : AbstractBody
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
