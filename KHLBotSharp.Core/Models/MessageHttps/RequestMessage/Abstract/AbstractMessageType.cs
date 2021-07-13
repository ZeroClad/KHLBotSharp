using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract
{
    public abstract class AbstractMessageType
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
