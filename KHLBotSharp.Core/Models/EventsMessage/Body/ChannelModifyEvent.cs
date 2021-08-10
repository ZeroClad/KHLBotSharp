using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.EventsMessage.Abstract;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.EventsMessage
{
    public class ChannelModifyEvent : AbstractBody
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("is_category")]
        public bool IsCategory { get; set; }
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }
        [JsonProperty("level")]
        public int? Level { get; set; }
        [JsonProperty("slow_mode")]
        public long SlowMode { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("permission_overwrites")]
        public IList<PermissionRole> PermissionOverwrites { get; set; }
        [JsonProperty("permission_users")]
        public IList<PermissionUser> PermissionUsers { get; set; }
        [JsonProperty("permission_sync")]
        [JsonConverter(typeof(BoolConverter))]
        public bool PermissionSync { get; set; }
    }
}
