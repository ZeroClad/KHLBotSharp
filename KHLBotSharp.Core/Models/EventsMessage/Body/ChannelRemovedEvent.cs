using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class ChannelRemovedEvent : AbstractBody
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("deleted_at")]
        public long DeletedAt { get; set; }
    }
}
