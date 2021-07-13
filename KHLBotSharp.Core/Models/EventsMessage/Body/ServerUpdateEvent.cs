using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.EventsMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage.Body
{
    public class ServerUpdateEvent: AbstractBody
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("notify_type")]
        public NotifyType NotifyType { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("enable_open")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsPublic { get; set; }
        [JsonProperty("open_id")]
        public string PublicId { get; set; }
        [JsonProperty("default_channel_id")]
        public string DefaultChannelId { get; set; }
        [JsonProperty("welcome_channel_id")]
        public string WelcomeChannelId { get; set; }
    }
}
