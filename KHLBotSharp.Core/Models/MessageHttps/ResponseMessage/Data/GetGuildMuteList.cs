using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class GetGuildMuteList : BaseData
    {
        [JsonProperty("1")]
        public IList<string> Mic { get; set; }
        [JsonProperty("2")]
        public IList<string> Headset { get; set; }
    }
}
