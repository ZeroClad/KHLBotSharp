﻿using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage.Text
{
    public class PrivateVideoMessageEvent:AbstractExtra
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("attachments")]
        public VideoAttachment Attachments { get; set; }
        [JsonProperty("author")]
        public User Author { get; set; }
    }

    public class VideoAttachment : Attachments
    {
        [JsonProperty("size")]
        public long Size { get; set; }
        [JsonProperty("duration")]
        public double Duration { get; set; }
        [JsonProperty("width")]
        public float Width { get; set; }
        [JsonProperty("height")]
        public float Height { get; set; }
    }
}
