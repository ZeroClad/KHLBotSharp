using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 私聊KMarkdown消息
    /// </summary>
    public class PrivateKMarkdownMessageEvent : PrivateTextMessageEvent
    {
        [JsonProperty("kmarkdown")]
        public KMarkdown KMarkdown { get; set; }
    }
}
