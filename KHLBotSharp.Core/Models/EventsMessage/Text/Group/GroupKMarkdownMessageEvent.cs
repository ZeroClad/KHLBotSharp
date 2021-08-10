using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class GroupKMarkdownMessageEvent : PrivateTextMessageEvent
    {
        [JsonProperty("kmarkdown")]
        public KMarkdown KMarkdown { get; set; }
    }
}
