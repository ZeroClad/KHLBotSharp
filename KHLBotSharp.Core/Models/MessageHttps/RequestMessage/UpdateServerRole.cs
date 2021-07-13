using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract;
using Newtonsoft.Json;
using System.Drawing;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage
{
    public class UpdateServerRole : AbstractMessageType
    {
        public UpdateServerRole(string GuildId, uint RoleId)
        {
            this.GuildId = GuildId;
            this.RoleId = RoleId;
        }
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("role_id")]
        public uint RoleId { get; set; }
        [JsonProperty("hoist")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Hoist { get; set; }
        [JsonProperty("mentionable")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Mentionable { get; set; }
        [JsonProperty("permissions")]
        public uint Permissions { get; set; }
        [JsonProperty("color")]
        [JsonConverter(typeof(Common.Converter.ColorConverter))]
        public Color Color { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
