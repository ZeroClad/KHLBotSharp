using Newtonsoft.Json.Linq;

namespace KHLBotSharp.WebHook.Net5.Services
{
    public interface IDecoderService
    {
        JObject DecodeEncrypt(JToken code);

        string GetEventType(JToken code);
    }
}
