using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.Objects
{
    public class Emoji : BaseData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("user_info")]
        public User User { get; set; }
    }
}
