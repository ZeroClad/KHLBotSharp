using KHLBotSharp.WebHook.Net5.Models.Webhook;

namespace KHLBotSharp.WebHook.Net5.Services
{
    public interface IWebhookInstanceManagerService
    {
        IWebhookInstanceManagerService Add(HookInstance obj);
        HookInstance Get(string name);
    }
}
