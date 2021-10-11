using Newtonsoft.Json.Linq;

namespace KHLBotSharp.Services
{
    public interface IDecoderService
    {
        JObject DecodeEncrypt(JToken code);

        string GetEventType(JToken code);
    }
}
