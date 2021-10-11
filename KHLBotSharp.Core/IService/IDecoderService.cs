using Newtonsoft.Json.Linq;

namespace KHLBotSharp.Services
{
    /// <summary>
    /// Webhook解析encrypt用
    /// </summary>
    public interface IDecoderService
    {
        /// <summary>
        /// 解析Encrypt
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        JObject DecodeEncrypt(JToken code);
        /// <summary>
        /// 获取事件类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        string GetEventType(JToken code);
    }
}
