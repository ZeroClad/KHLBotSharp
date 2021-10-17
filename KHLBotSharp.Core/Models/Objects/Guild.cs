using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.Objects
{
    /// <summary>
    /// 服务器
    /// </summary>
    public class Guild : BaseData
    {
        /// <summary>
        /// 服务器ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 服务器名字
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 服务器主题
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; }
        /// <summary>
        /// 服务器主Id
        /// </summary>
        [JsonProperty("master_id")]
        public string MasterId { get; set; }
        /// <summary>
        /// 服务器Icon网址
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        [JsonProperty("notify_type")]
        public NotifyType NotifyType { get; set; }
        /// <summary>
        /// 默认语音地域
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        [JsonProperty("enable_open")]
        public bool OpenPublic { get; set; }
        /// <summary>
        /// 公开服务器Id
        /// </summary>
        [JsonProperty("open_id")]
        public string OpenId { get; set; }
        /// <summary>
        /// 默认频道Id
        /// </summary>
        [JsonProperty("default_channel_id")]
        public string DefaultChannelId { get; set; }
        /// <summary>
        /// 欢迎频道Id
        /// </summary>
        [JsonProperty("welcome_channel_id")]
        public string WelcomeChannelId { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        [JsonProperty("roles")]
        public IList<Role> Roles { get; set; }
        /// <summary>
        /// 频道列表
        /// </summary>
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
