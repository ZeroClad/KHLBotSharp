using KHLBotSharp.Models.EventsMessage.Abstract;
using KHLBotSharp.Models.MessageHttps.EventMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage.Body
{
    public class UserInfoChangeEvent:AbstractBody
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }
}
