﻿using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage.Body
{
    public class CardMessageButtonClickEvent:AbstractBody
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("target_id")]
        public string TargetId { get; set; }
    }
}
