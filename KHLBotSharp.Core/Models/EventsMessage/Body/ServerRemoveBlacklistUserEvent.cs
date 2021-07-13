using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.EventsMessage.Body
{
    public class ServerRemoveBlacklistUserEvent:AbstractBody
    {
        [JsonProperty("operator_id")]
        public string OperatorId { get; set; }
        [JsonProperty("user_id")]
        public IList<string> UserIds { get; set;}
    }
}
