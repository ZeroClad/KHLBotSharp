using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 群文件消息
    /// </summary>
    public class GroupFileMessageEvent : AttachmentMessageExtra
    {
        [JsonProperty("attachments")]
        public FileAttachment Attachments { get; set; }
    }
}
