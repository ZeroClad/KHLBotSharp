using KHLBotSharp.Models.MessageHttps.ResponseMessage.Data.Abstract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.MessageHttps.ResponseMessage.Data
{
    public class GetServerRoleList : BaseData
    {
        [JsonProperty("items")]
        public IList<ServerRole> Items { get; set; }
    }
}
