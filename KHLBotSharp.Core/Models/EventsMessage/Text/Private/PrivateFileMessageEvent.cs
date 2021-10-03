using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 私聊文件消息
    /// </summary>
    public class PrivateFileMessageEvent : AbstractExtra
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("attachments")]
        public FileAttachment Attachments { get; set; }
        [JsonProperty("author")]
        public User Author { get; set; }
    }
    /// <summary>
    /// 文件资料
    /// </summary>
    public class FileAttachment : Attachments
    {
        [JsonProperty("size")]
        public long Size { get; set; }
    }
}
