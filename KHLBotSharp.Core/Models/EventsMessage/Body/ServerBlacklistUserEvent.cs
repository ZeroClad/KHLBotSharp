using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.EventsMessage.Body
{
    public class ServerBlacklistUserEvent:AbstractBody
    {
        [JsonProperty("operator_id")]
        public string OperatorId { get; set; }
        [JsonProperty("remark")]
        public string Remark { get; set; }
        [JsonProperty("user_id")]
        public IList<string> UserIds { get; set; }
    }
}
