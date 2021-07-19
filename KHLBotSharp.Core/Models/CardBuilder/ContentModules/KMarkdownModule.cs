using Newtonsoft.Json;
namespace KHLBotSharp.Core.Models
{
    public class KMarkdownModule : ICardTextComponent
    {
        public string Type => "kmarkdown";
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
