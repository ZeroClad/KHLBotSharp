using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class UploadFileResponse:BaseData
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
