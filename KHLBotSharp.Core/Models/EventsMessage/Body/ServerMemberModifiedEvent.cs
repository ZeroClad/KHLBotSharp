using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    public class ServerMemberModifiedEvent : AbstractBody
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
    }
}
