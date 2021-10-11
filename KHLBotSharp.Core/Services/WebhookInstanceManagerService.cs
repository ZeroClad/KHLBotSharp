using KHLBotSharp.Core.BotHost;
using System.Collections.Generic;
using System.Linq;


namespace KHLBotSharp.Services
{
    public class WebhookInstanceManagerService : IWebhookInstanceManagerService
    {
        private readonly IList<WebHookInstance> hookInstances = new List<WebHookInstance>();
        public IWebhookInstanceManagerService Add(WebHookInstance obj)
        {
            hookInstances.Add(obj);
            return this;
        }

        public WebHookInstance Get(string name)
        {
            if(name == null)
            {
                return hookInstances.First();
            }
            else
            {
                return hookInstances.FirstOrDefault(x => x.Name == name);
            }
        }

        public IList<WebHookInstance> HookInstances
        {
            get
            {
                return hookInstances;
            }
        }
    }
}
