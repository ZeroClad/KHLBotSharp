using Newtonsoft.Json;

namespace KHLBotSharp.Models.Objects
{
    public abstract class Attachments
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// 文件名字
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 文件在开黑啦服务器的url
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
