using KHLBotSharp.Core.Models.Objects;
using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public class SendMessage : AbstractMessageType
    {
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<GroupTextMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<PrivateTextMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<GroupCardMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<PrivateCardMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<GroupFileMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<PrivateFileMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<GroupKMarkdownMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<PrivateKMarkdownMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<GroupPictureMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<PrivatePictureMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<GroupVideoMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 回复上一个消息，自动把消息的数据转换
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="quote"></param>
        /// <param name="tempMessage"></param>
        public SendMessage(ReceiveMessageData<PrivateVideoMessageEvent> request, string content, bool quote = true, bool tempMessage = false)
        {
            Set(request, content, quote);
            if (tempMessage)
            {
                TempTargetId = request.Extra.Author.Id;
            }
        }
        /// <summary>
        /// 创建新消息
        /// </summary>
        public SendMessage()
        {
            Type = MessageType.TextMessage;
        }

        private void Set<T>(ReceiveMessageData<T> request, string content, bool quote = true) where T : Extra
        {
            Type = request.Type;
            TargetId = request.TargetId;
            Content = content;
            if (quote)
            {
                Quote = request.MsgId;
            }
            Nonce = request.Nonce;
            //Card Message
            if (Content.StartsWith("[") && Content.EndsWith("]"))
            {
                Type = MessageType.CardMessage;
            }
        }
        /// <summary>
        /// 消息种类
        /// </summary>
        [JsonProperty("type")]
        public MessageType Type { get; set; }
        /// <summary>
        /// 发送目标群或者私聊Id
        /// </summary>
        [JsonProperty("target_id")]
        public string TargetId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
        /// <summary>
        /// 艾特
        /// </summary>
        [JsonProperty("quote")]
        public string Quote { get; set; }
        /// <summary>
        /// 无需知道，反正跟着用就行
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        /// <summary>
        /// 发送Temp消息，不储存到数据库
        /// </summary>
        [JsonProperty("temp_target_id")]
        public string TempTargetId { get; set; }
    }
}
