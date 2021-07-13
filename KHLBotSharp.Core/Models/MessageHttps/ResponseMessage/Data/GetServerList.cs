using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class GetServerList:BaseData
    {
        [JsonProperty("items")]
        public IList<Guild> Items { get; set; }
    }
}
