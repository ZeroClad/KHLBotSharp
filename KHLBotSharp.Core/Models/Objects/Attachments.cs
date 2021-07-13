using Newtonsoft.Json;

namespace KHLBotSharp.Models.Objects
{
    public abstract class Attachments
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
