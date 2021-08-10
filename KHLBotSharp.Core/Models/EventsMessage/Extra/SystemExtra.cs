using KHLBotSharp.Models.EventsMessage.Abstract;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class SystemExtra<T> : AbstractExtra where T : AbstractBody
    {
        [JsonProperty("body")]
        public T Body { get; set; }
    }
}
