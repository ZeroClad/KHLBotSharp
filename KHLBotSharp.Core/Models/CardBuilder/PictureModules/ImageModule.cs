using Newtonsoft.Json;

namespace KHLBotSharp.Core.Models
{
    public class ImageModule : ICardImageComponent
    {
        public string Type => "image";
        [JsonProperty("src")]
        public string Src { get; set; }
    }
}
