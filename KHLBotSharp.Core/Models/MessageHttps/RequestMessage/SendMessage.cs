using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage
{
    public class SendMessage : AbstractMessageType
    {
        public SendMessage(ReceiveMessageData<PrivateTextMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Type = request.Type;
            TargetId = request.TargetId;
            Content = content;
            if (quote)
            {
                Quote = request.MsgId;
            }
            Nonce = request.Nonce;
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
            //Card Message
            if (Content.StartsWith("[") && Content.EndsWith("]"))
            {
                Type = 10;
            }
        }
        public SendMessage(ReceiveMessageData<GroupTextMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Type = request.Type;
            TargetId = request.TargetId;
            Content = content;
            if (quote)
            {
                Quote = request.MsgId;
            }
            Nonce = request.Nonce;
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
            //Card Message
            if (Content.StartsWith("[") && Content.EndsWith("]"))
            {
                Type = 10;
            }
        }

        public SendMessage()
        {
            Type = 1;
        }

        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("target_id")]
        public string TargetId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("quote")]
        public string Quote { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("temp_target_id")]
        public string TempTargetId { get; set; }
    }
}
