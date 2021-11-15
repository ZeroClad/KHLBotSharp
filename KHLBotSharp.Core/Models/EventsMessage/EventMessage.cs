using KHLBotSharp.Core.Models.Objects;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.MessageHttps.RequestMessage;
using Newtonsoft.Json;
using System;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventMessage<T> : EventMessage where T : Extra
    {
        /// <summary>
        /// 事件数据
        /// </summary>
        [JsonProperty("d")]
        public new ReceiveMessageData<T> Data { get; set; }
        /// <summary>
        /// 创建回复用SendMessage
        /// </summary>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <param name="reply"></param>
        /// <param name="tempMessage"></param>
        /// <returns></returns>
        public SendMessage CreateReply(string content, bool reply = true, bool tempMessage = false, MessageType type = Core.Models.Objects.MessageType.TextMessage)
        {
            return Data.CreateReply(content, reply, tempMessage, type);
        }
    }
    /// <summary>
    /// 事件消息
    /// </summary>
    public class EventMessage
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty("s")]
        public int MessageType { get; set; }
        /// <summary>
        /// 消息标记
        /// </summary>
        [JsonProperty("sn")]
        public int SerialNumber { get; set; }
        /// <summary>
        /// 事件数据
        /// </summary>
        public object Data { get; set; }
    }

    public class ReceiveMessageData<T> : ReceiveMessageData where T : Extra
    {
        /// <summary>
        /// 不同的消息类型，结构不一致
        /// </summary>
        public T Extra { get; set; }
        /// <summary>
        /// 创建回复用SendMessage
        /// </summary>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <param name="reply"></param>
        /// <param name="tempMessage"></param>
        /// <returns></returns>
        public SendMessage CreateReply(string content, bool reply = true, bool tempMessage = false, MessageType type = MessageType.TextMessage)
        {
            if (typeof(GroupTextMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<GroupTextMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(GroupCardMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<GroupCardMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(GroupFileMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<GroupFileMessageEvent>, content, reply, tempMessage) { TypeV2 = type };
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(GroupKMarkdownMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<GroupKMarkdownMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(GroupPictureMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<GroupPictureMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(GroupVideoMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<GroupVideoMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(PrivateCardMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<PrivateCardMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(PrivateFileMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<PrivateFileMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(PrivateKMarkdownMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<PrivateKMarkdownMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(PrivatePictureMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<PrivatePictureMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(PrivateTextMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<PrivateTextMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            else if (typeof(PrivateVideoMessageEvent).IsAssignableFrom(typeof(T)))
            {
                var result = new SendMessage(this as ReceiveMessageData<PrivateVideoMessageEvent>, content, reply, tempMessage);
                if (!(result.TypeV2 == MessageType.CardMessage && type == MessageType.TextMessage))
                {
                    result.TypeV2 = type;
                }
                return result;
            }
            throw new InvalidOperationException("Not supported object type for creating reply. Only commands (text, card, kmarkdown, file, picture or video message) are available");
        }
    }
    /// <summary>
    /// 收到的消息资料
    /// </summary>
    public class ReceiveMessageData
    {
        /// <summary>
        /// 消息频道类型, GROUP 为频道消息
        /// </summary>
        [JsonProperty("channel_type")]
        public string ChannelType { get; set; }
        /// <summary>
        /// 消息种类，1:文字消息, 2:图片消息，3:视频消息，4:文件消息， 8:音频消息，9:KMarkdown，10:card消息，255:系统消息
        /// </summary>
        [JsonProperty("type")]
        public MessageType Type { get; set; }
        /// <summary>
        /// 发送目的 id，如果为是 GROUP 消息，则 target_id 代表频道 id
        /// </summary>
        [JsonProperty("target_id")]
        public string TargetId { get; set; }
        /// <summary>
        /// 发送者 id, 1 代表系统
        /// </summary>
        [JsonProperty("author_id")]
        public string AuthorId { get; set; }
        /// <summary>
        /// 消息内容, 文件，图片，视频时，content 为 url
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
        /// <summary>
        /// 消息的 id
        /// </summary>
        [JsonProperty("msg_id")]
        public string MsgId { get; set; }
        /// <summary>
        /// 消息发送时间的毫秒时间戳
        /// </summary>
        [JsonProperty("msg_timestamp")]
        public long MsgTimestamp { get; set; }
        /// <summary>
        /// 随机串，与用户消息发送 api 中传的 nonce 保持一致
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        /// <summary>
        /// 不知道干啥用的
        /// </summary>
        [JsonProperty("verify_token")]
        public string VerifyToken { get; set; }
    }
}
