using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.Objects
{
    public class Guild : BaseData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("master_id")]
        public string MasterId { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("notify_type")]
        public NotifyType NotifyType { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("enable_open")]
        public bool OpenPublic { get; set; }
        [JsonProperty("open_id")]
        public string OpenId { get; set; }
        [JsonProperty("default_channel_id")]
        public string DefaultChannelId { get; set; }
        [JsonProperty("welcome_channel_id")]
        public string WelcomeChannelId { get; set; }
        [JsonProperty("roles")]
        public IList<Role> Roles { get; set; }
        [JsonProperty("channels")]
        public IList<Channel> Channels { get; set; }
    }
    /// <summary>
    /// 通知类型, 0代表默认使用服务器通知设置，1代表接收所有通知, 2代表仅@被提及，3代表不接收通知
    /// </summary>
    public enum NotifyType
    {
        Default,
        All,
        At,
        No
    }
}
