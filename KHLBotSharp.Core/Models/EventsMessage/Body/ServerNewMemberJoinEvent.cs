using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class ServerNewMemberJoinEvent : AbstractBody
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("joined_at")]
        public long JoinedAt { get; set; }
    }
}
