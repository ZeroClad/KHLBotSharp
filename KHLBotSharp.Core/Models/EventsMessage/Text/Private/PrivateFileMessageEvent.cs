using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 私聊文件消息
    /// </summary>
    public class PrivateFileMessageEvent : AttachmentMessageExtra
    {
        [JsonProperty("attachments")]
        public FileAttachment Attachments { get; set; }
    }

}
