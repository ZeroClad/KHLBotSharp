﻿using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 群KMarkdown消息
    /// </summary>
    public class GroupKMarkdownMessageEvent : TextMessageExtra
    {
        [JsonProperty("kmarkdown")]
        public KMarkdown KMarkdown { get; set; }
    }
}
