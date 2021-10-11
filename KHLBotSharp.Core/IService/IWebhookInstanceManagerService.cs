using KHLBotSharp.Core.BotHost;
using System.ComponentModel;

namespace KHLBotSharp.Services
{
    /// <summary>
    /// 内部使用，无需搞懂
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IWebhookInstanceManagerService
    {
        IWebhookInstanceManagerService Add(WebHookInstance obj);
        WebHookInstance Get(string name);
    }
}
