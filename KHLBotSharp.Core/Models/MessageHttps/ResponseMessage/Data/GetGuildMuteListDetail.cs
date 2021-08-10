using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class GetGuildMuteListDetail : BaseData
    {
        [JsonProperty("mic")]
        public MuteListObject Mic { get; set; }
        [JsonProperty("headset")]
        public MuteListObject Headset { get; set; }
    }

    public class MuteListObject
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("user_ids")]
        public IList<string> UserIds { get; set; }
    }
}
