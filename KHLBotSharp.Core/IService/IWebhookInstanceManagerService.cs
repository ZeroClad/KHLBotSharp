using KHLBotSharp.Core.BotHost;

namespace KHLBotSharp.Services
{
    public interface IWebhookInstanceManagerService
    {
        IWebhookInstanceManagerService Add(WebHookInstance obj);
        WebHookInstance Get(string name);
    }
}
