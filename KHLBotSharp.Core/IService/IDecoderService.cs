using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

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
        Task<JObject> DecodeEncrypt(JToken code);
        /// <summary>
        /// 获取事件类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<string> GetEventType(JToken code);
    }
}
