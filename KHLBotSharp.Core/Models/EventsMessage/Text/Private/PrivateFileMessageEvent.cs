using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage.Text
{
    public class PrivateFileMessageEvent:AbstractExtra
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("attachments")]
        public FileAttachment Attachments { get; set; }
        [JsonProperty("author")]
        public User Author { get; set; }
    }

    public class FileAttachment : Attachments
    {
        [JsonProperty("size")]
        public long Size { get; set; }
    }
}
