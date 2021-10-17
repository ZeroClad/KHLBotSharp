using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.Objects
{
    /// <summary>
    /// KMarkdown
    /// </summary>
    public class KMarkdown
    {
        /// <summary>
        /// KMarkdown文本内容
        /// </summary>
        [JsonProperty("raw_content")]
        public string RawContent { get; set; }
        /// <summary>
        /// 暂时文档不足，无法得知详情，故由Object取代
        /// </summary>
        [JsonProperty("mention_part")]
        public IList<object> MentionPart { get; set; }
        /// <summary>
        /// 暂时文档不足，无法得知详情，故由Object取代
        /// </summary>
        [JsonProperty("mention_role_part")]
        public IList<object> MentionRolePart { get; set; }
    }
}
