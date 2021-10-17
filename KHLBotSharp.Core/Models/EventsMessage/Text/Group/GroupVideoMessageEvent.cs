﻿using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 群影片/视频消息
    /// </summary>
    public class GroupVideoMessageEvent : AttachmentMessageExtra
    {
        /// <summary>
        /// 视频/影片
        /// </summary>
        [JsonProperty("attachments")]
        public VideoAttachment Attachments { get; set; }
    }
}
