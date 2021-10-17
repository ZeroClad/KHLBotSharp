using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.Objects
{
    /// <summary>
    /// 频道详情
    /// </summary>
    public class Channel : BaseData
    {
        /// <summary>
        /// 频道ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 频道名字
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        /// <summary>
        /// 服务器ID
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        /// <summary>
        /// 聊的内容
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; }
        /// <summary>
        /// 是否为频道分组
        /// </summary>
        [JsonProperty("is_category")]
        public bool IsCategory { get; set; }
        /// <summary>
        /// 频道分组ID
        /// </summary>
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }
        /// <summary>
        /// 排行
        /// </summary>
        [JsonProperty("level")]
        public int Level { get; set; }
        /// <summary>
        /// 是否慢速，以及慢速的话多少秒才可发消息
        /// </summary>
        [JsonProperty("slow_mode")]
        public int SlowModeSec { get; set; }
        /// <summary>
        /// 频道类型
        /// </summary>
        [JsonProperty("type")]
        public ChannelType ChannelType { get; set; }
        /// <summary>
        /// 权限是否跟服务器统一
        /// </summary>
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
