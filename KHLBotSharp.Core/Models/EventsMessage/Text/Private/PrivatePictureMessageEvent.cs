using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 私聊图片消息
    /// </summary>
    public class PrivatePictureMessageEvent : AbstractExtra
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("attachments")]
        public PictureAttachments Attachments { get; set; }
        [JsonProperty("author")]
        public User Author { get; set; }

    }
    /// <summary>
    /// 图片文件资料
    /// </summary>
    public class PictureAttachments : Attachments
    {

    }
}
