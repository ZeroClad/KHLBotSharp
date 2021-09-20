using KHLBotSharp.Common.Permission;
using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class ServerRole : BaseData
    {
        [JsonProperty("role_id")]
        public uint RoleId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("color")]
        [JsonConverter(typeof(Common.Converter.ColorConverter))]
        public Color Color { get; set; }

        [JsonProperty("position")]
        public uint Position { get; set; }
        [JsonProperty("hoist")]
        [JsonConverter(typeof(Common.Converter.BoolConverter))]
        public bool Hoist { get; set; }
        [JsonProperty("mentionable")]
        [JsonConverter(typeof(Common.Converter.BoolConverter))]
        public bool Mentionable { get; set; }
        [JsonProperty("permissions")]
        public uint Permissions { get; set; }
        [JsonIgnore]
        public IEnumerable<Permission> ParsedPermission
        {
            get
            {
                return Permissions.ParsePermissions();
            }
        }
    }
}
