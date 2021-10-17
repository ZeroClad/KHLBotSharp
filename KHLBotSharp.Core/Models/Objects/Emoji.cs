using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.Objects
{
    /// <summary>
    /// 表情
    /// </summary>
    public class Emoji : BaseData
    {
        /// <summary>
        /// 表情ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 表情名字
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 上传用户
        /// </summary>
        [JsonProperty("user_info")]
        public User User { get; set; }
    }
}
