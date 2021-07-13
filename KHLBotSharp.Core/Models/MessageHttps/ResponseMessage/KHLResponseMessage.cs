using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage
{
    public class KHLResponseMessage<T>: KHLResponseMessage where T : BaseData
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
    public class KHLResponseMessage
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }
    }
}
