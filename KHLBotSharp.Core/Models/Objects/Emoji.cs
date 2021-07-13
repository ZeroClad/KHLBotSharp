using Newtonsoft.Json;

namespace KHLBotSharp.Models.Objects
{
    public class Emoji
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
