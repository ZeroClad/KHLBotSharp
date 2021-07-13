using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage.Body
{
    public class ServerMemberExitEvent:AbstractBody
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("exited_at")]
        public long ExitedAt { get; set; }
    }
}
