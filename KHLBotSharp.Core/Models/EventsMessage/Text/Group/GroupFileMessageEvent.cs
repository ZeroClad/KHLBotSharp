using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage.Text
{
    public class GroupFileMessageEvent:AbstractExtra
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("attachments")]
        public GroupFileAttachment Attachments { get; set; }
        [JsonProperty("author")]
        public User Author { get; set; }
    }

    public class GroupFileAttachment : Attachments
    {
        [JsonProperty("size")]
        public long Size { get; set; }
    }
}
