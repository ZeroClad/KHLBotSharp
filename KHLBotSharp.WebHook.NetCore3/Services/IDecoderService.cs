using Newtonsoft.Json.Linq;

namespace KHLBotSharp.WebHook.NetCore3.Services
{
    public interface IDecoderService
    {
        JObject DecodeEncrypt(JToken code);

        string GetEventType(JToken code);
    }
}
