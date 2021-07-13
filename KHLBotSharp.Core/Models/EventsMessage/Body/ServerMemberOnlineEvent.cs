using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.EventsMessage.Body
{
    public class ServerMemberOnlineEvent:AbstractBody
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("event_time")]
        public long EventTime { get; set; }
        [JsonProperty("guilds")]
        public IList<string> Guilds { get; set; }
    }
}
