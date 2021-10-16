using Newtonsoft.Json;

namespace KHLBotSharp.Core.Models
{
    public class TextModule : ICardTextComponent
    {
        [JsonProperty("type")]
        public string Type => "plain-text";
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
