using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class GetServerMemberReply:BaseData
    {
        [JsonProperty("items")]
        public IList<User> Items { get; set; }
        [JsonProperty("user_count")]
        public int UserCount { get; set; }
        [JsonProperty("online_count")]
        public int OnlineCount { get; set; }
        [JsonProperty("offline_count")]
        public int OfflineCount { get; set; }
    }
}
