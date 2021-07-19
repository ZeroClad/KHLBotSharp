using Newtonsoft.Json;

namespace KHLBotSharp.Core.Models
{
    public class HeaderModule : ICardBodyComponent
    {
        public string Type => "header";
        [JsonProperty("text")]
        public TextModule Text { get; set; }
    }
}
