using KHLBotSharp.Models.EventsMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class PrivateMessageAddReactionEvent : AbstractBody
    {
        [JsonProperty("emoji")]
        public Emoji Emoji { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("chat_code")]
        public string ChatCode { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
    }
}
