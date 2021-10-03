using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 影片/视频消息
    /// </summary>
    public class PrivateVideoMessageEvent : AbstractExtra
    {
        /// <summary>
        /// 服务器ID
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        /// <summary>
        /// 我自己都忘记是啥了
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// 视频/影片
        /// </summary>
        [JsonProperty("attachments")]
        public VideoAttachment Attachments { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        [JsonProperty("author")]
        public User Author { get; set; }
    }
    /// <summary>
    /// 影片/视频详情
    /// </summary>
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
