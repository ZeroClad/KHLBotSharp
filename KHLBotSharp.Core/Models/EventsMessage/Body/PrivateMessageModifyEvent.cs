using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class PrivateMessageModifyEvent : AbstractBody
    {
        [JsonProperty("author_id")]
        public string AuthorId { get; set; }
        [JsonProperty("target_id")]
        public string TargetId { get; set; }
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }
        [JsonProperty("chat_code")]
        public long ChatCode { get; set; }
    }
}
