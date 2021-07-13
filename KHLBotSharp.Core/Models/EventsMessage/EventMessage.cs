using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class EventMessage<T>: EventMessage where T : AbstractExtra
    {
        [JsonProperty("d")]
        public ReceiveMessageData<T> Data { get; set; }
    }

    public class EventMessage
    {
        [JsonProperty("s")]
        public int MessageType { get; set; }
        [JsonProperty("sn")]
        public int SerialNumber { get; set; }
    }

    public class ReceiveMessageData<T>: ReceiveMessageData where T : AbstractExtra
    {
        public T Extra { get; set; }
    }

    public class ReceiveMessageData
    {
        [JsonProperty("channel_type")]
        public string ChannelType { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("target_id")]
        public string TargetId { get; set; }
        [JsonProperty("author_id")]
        public string AuthorId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("msg_id")]
        public string MsgId { get; set; }
        [JsonProperty("msg_timestamp")]
        public long MsgTimestamp { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("verify_token")]
        public string VerifyToken { get; set; }
    }
}
