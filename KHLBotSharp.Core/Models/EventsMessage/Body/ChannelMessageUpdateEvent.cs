using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 频道消息更新事件
    /// </summary>
    public class ChannelMessageUpdateEvent : AbstractBody
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("mention")]
        public IList<string> Mention { get; set; }
        [JsonProperty("mention_all")]
        public bool MentionAll { get; set; }
        [JsonProperty("mention_here")]
        public bool MentionHere { get; set; }
        [JsonProperty("mention_roles")]
        public IList<string> MentionRoles { get; set; }
        [JsonProperty("updated_at")]
        public long UpdatedTimestamp { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
    }
}
