using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 私聊图片消息
    /// </summary>
    public class PrivatePictureMessageEvent : AttachmentMessageExtra
    {
        [JsonProperty("attachments")]
        public PictureAttachments Attachments { get; set; }
    }

}
