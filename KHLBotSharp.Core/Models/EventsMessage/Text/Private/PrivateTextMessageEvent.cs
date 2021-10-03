using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 私聊文字消息
    /// </summary>
    public class PrivateTextMessageEvent : AbstractExtra
    {
        /// <summary>
        /// 私聊服务器ID
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        /// <summary>
        /// 私聊频道名字
        /// </summary>
        [JsonProperty("channel_name")]
        public string ChannelName { get; set; }
        /// <summary>
        /// 艾特的人
        /// </summary>
        [JsonProperty("mention")]
        public IList<string> Mention { get; set; }
        /// <summary>
        /// 艾特全体成员
        /// </summary>
        [JsonProperty("mention_all")]
        public bool MentionAll { get; set; }
        /// <summary>
        /// 艾特的特定角色
        /// </summary>
        [JsonProperty("mention_roles")]
        public IList<long> MentionRoles { get; set; }
        /// <summary>
        /// 艾特在线的人
        /// </summary>
        [JsonProperty("mention_here")]
        public bool MentionHere { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        [JsonProperty("author")]
        public User Author { get; set; }
    }
}
