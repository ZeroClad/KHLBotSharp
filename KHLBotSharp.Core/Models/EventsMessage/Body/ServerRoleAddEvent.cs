using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;
using System.Drawing;

namespace KHLBotSharp.Models.EventsMessage
{
    public class ServerRoleAddEvent : AbstractBody
    {
        [JsonProperty("role_id")]
        public int RoleId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("color")]
        [JsonConverter(typeof(Common.Converter.ColorConverter))]
        public Color Color { get; set; }
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("hoist")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Hoist { get; set; }
        [JsonProperty("mentionable")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Mentionable { get; set; }
        [JsonProperty("permissions")]
        public int Permissions { get; set; }
    }
}
