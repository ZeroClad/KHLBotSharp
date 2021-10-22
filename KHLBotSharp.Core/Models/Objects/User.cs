using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.Objects
{
    /// <summary>
    /// 用户详情
    /// </summary>
    public class User : BaseData
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }
        /// <summary>
        /// 用户识别号 （#开头的）
        /// </summary>
        [JsonProperty("identify_num")]
        public string IdentifyNumber { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        [JsonProperty("online")]
        public bool Online { get; set; }
        /// <summary>
        /// 账号当前状态
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }
        /// <summary>
        /// 头像链接
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        /// <summary>
        /// 是否机器人
        /// </summary>
        [JsonProperty("bot")]
        public bool IsBot { get; set; }
        /// <summary>
        /// 是否验证了手机号
        /// </summary>
        [JsonProperty("mobile_verified")]
        public bool IsMobileVerified { get; set; }
        /// <summary>
        /// 是否系统号
        /// </summary>
        [JsonProperty("system")]
        public bool IsSystem { get; set; }
        /// <summary>
        /// 手机位置
        /// </summary>
        [JsonProperty("mobile_prefix")]
        public string MobilePrefix { get; set; }
        /// <summary>
        /// 手机号，已马赛克
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        /// <summary>
        /// 邀请过多少人
        /// </summary>
        [JsonProperty("invited_count")]
        public int InvitedCount { get; set; }
        /// <summary>
        /// 群昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string Nick { get; set; }
        /// <summary>
        /// 群里拥有的权限
        /// </summary>
        [JsonProperty("roles")]
        public IList<uint> Roles { get; set; }
    }

    public enum Status
    {
        Normal = 0,
        Banned = 10
    }

}
