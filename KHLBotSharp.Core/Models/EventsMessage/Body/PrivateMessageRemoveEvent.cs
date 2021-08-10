using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class PrivateMessageRemoveEvent : AbstractBody
    {
        [JsonProperty("chat_code")]
        public string ChatCode { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
        [JsonProperty("author_id")]
        public string AuthorId { get; set; }
        [JsonProperty("target_id")]
        public string TargetId { get; set; }
        [JsonProperty("deleted_at")]
        public long DeletedAt { get; set; }
    }
}
