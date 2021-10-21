using KHLBotSharp.Models.EventsMessage.Abstract;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 系统消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SystemExtra<T> : Extra where T : AbstractBody
    {
        /// <summary>
        /// 系统消息详情
        /// </summary>
        [JsonProperty("body")]
        public T Body { get; set; }
        public new string Type { get; set; }
    }
}
