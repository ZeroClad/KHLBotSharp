using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.Objects
{
    public class User : BaseData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("identify_num")]
        public string IdentifyNumber { get; set; }
        [JsonProperty("online")]
        public bool Online { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("bot")]
        public bool IsBot { get; set; }
        [JsonProperty("mobile_verified")]
        public bool IsMobileVerified { get; set; }
        [JsonProperty("system")]
        public bool IsSystem { get; set; }
        [JsonProperty("mobile_prefix")]
        public string MobilePrefix { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [JsonProperty("invited_count")]
        public int InvitedCount { get; set; }
        [JsonProperty("nickname")]
        public string Nick { get; set; }
        [JsonProperty("roles")]
        public IList<uint> Roles { get; set; }
    }

    public enum Status 
    { 
        Normal = 0,
        Banned = 10
    }

}
