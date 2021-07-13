using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class PrivateMessageList:BaseData
    {
        [JsonProperty("items")]
        public IList<PrivateMessageDetail> Items { get; set; }
    }

    public class PrivateMessageDetail:BaseData
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("last_read_time")]
        public long LastReadTime { get; set; }
        [JsonProperty("latest_msg_time")]
        public long LastMessageTime { get; set; }
        [JsonProperty("unread_count")]
        public long UnreadCount { get; set; }
        [JsonProperty("target_info")]
        public User TargetInfo { get; set; }
    }
}
