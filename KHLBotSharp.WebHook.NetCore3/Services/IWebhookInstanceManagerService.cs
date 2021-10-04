using KHLBotSharp.WebHook.NetCore3.Models.Webhook;

namespace KHLBotSharp.WebHook.NetCore3.Services
{
    public interface IWebhookInstanceManagerService
    {
        IWebhookInstanceManagerService Add(HookInstance obj);
        HookInstance Get(string name);
    }
}
