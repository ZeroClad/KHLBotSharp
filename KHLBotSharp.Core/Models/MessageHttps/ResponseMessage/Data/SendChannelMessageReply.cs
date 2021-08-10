using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class SendChannelMessageReply : BaseData
    {
        [JsonProperty("msg_id")]
        public string MessageId { get; set; }
        [JsonProperty("msg_timestamp")]
        public long MessageTimeStamp { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }
}
