using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.Objects
{
    public class Channel:BaseData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("is_category")]
        public bool IsCategory { get; set; }
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("slow_mode")]
        public int SlowModeSec { get; set; }
        [JsonProperty("type")]
        public ChannelType ChannelType { get; set; }
        [JsonProperty("permission_sync")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsPermissionSync { get; set; }
    }
    /// <summary>
    /// 频道类型: 1 文字频道, 2 语音频道
    /// </summary>
    public enum ChannelType
    {
        Text = 1,
        Voice = 2
    }
}
