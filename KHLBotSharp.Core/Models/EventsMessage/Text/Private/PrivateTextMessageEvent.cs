using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.EventsMessage.Text
{
    public class PrivateTextMessageEvent:AbstractExtra
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("channel_name")]
        public string ChannelName { get; set; }
        [JsonProperty("mention")]
        public IList<string> Mention { get; set; }
        [JsonProperty("mention_all")]
        public bool MentionAll { get; set; }
        [JsonProperty("mention_roles")]
        public IList<long> MentionRoles { get; set; }
        [JsonProperty("mention_here")]
        public bool MentionHere { get; set; }
        [JsonProperty("author")]
        public User Author { get; set; }
    }
}
