using KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage
{
    public class EditMessage : AbstractMessageType
    {
        public EditMessage(string msgId, string content)
        {
            MsgId = msgId;
            Content = content;
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
