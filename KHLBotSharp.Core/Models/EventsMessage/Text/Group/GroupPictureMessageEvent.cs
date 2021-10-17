using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 群图片消息
    /// </summary>
    public class GroupPictureMessageEvent : AttachmentMessageExtra
    {

        [JsonProperty("attachments")]
        public PictureAttachments Attachments { get; set; }
    }
}
