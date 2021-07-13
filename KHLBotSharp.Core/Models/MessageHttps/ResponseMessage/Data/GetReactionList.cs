using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class GetReactionList : BaseData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        [JsonProperty("identify_num")]
        public string IdentifyNum { get; set; }
        [JsonProperty("online")]
        public bool IsOnline { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("bot")]
        public string IsBot { get; set; }
        [JsonProperty("reaction_time")]
        public long ReactionTime { get; set; }
    }
}
