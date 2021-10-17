using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Drawing;

namespace KHLBotSharp.Models.Objects
{
    /// <summary>
    /// 角色详情
    /// </summary>
    public class Role : BaseData
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [JsonProperty("role_id")]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 角色颜色
        /// </summary>
        [JsonProperty("color")]
        [JsonConverter(typeof(ColorConverter))]
        public Color Color { get; set; }
        /// <summary>
        /// 角色位置，越高则包含往下所有的权限并且可控制比自己权限低的
        /// </summary>
        [JsonProperty("position")]
        public int Position { get; set; }
        /// <summary>
        /// 不知道啥玩意
        /// </summary>
        [JsonProperty("hoist")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Hoist { get; set; }
        /// <summary>
        /// 是否可单独艾特
        /// </summary>
        [JsonProperty("mentionable")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Mentionable { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [JsonProperty("permissions")]
        public int Permissions { get; set; }
    }
}
