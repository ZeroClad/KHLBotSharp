using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.EventMessage.Abstract
{
    public abstract class AbstractExtra
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
