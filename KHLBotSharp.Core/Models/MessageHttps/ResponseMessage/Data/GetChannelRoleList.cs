using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class GetChannelRoleList : BaseData
    {
        [JsonProperty("permission_overwrites")]
        public IList<PermissionRole> Roles { get; set; }
        [JsonProperty("permission_users")]
        public IList<PermissionUser> Users { get; set; }
        [JsonProperty("permission_sync")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsPermissionSync { get; set; }
    }

    public class PermissionUser
    {
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("allow")]
        public int Allow { get; set; }
        [JsonProperty("deny")]
        public int Deny { get; set; }
    }

    public class PermissionRole
    {
        [JsonProperty("role_id")]
        public int RoleId { get; set; }
        [JsonProperty("allow")]
        public int Allow { get; set; }
        [JsonProperty("deny")]
        public int Deny { get; set; }
    }
}
