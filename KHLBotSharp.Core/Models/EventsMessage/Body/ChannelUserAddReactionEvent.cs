using KHLBotSharp.Models.EventsMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class ChannelUserAddReactionEvent : AbstractBody
    {
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
    }
}
