using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class ChannelPinnedMessageEvent : AbstractBody
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty("operator_id")]
        public long OperatorId { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
    }
}
