using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KHLBotSharp.Models.CardBuilder
{
    public class Card
    {
        [JsonProperty("type")]
        public string Type { get; } = "card";
        [JsonProperty("size")]
        public string Size { get; set; } = "sm";
        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }
    }
}
