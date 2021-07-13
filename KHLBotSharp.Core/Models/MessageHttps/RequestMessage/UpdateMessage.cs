using KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage
{
    public class UpdateMessage: AbstractMessageType
    {
        public UpdateMessage(string Quote, string Content)
        {
            this.MsgId = Quote;
            this.Content = Content;
        }
        [JsonProperty("msg_id")]
        public string MsgId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("quote")]
        public string Quote { get; set; }
        [JsonProperty("temp_target_id")]
        public string TempTargetId { get; set; }
    }
}
