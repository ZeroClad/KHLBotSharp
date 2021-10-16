using KHLBotSharp.Core.BotHost;
using System.Collections.Generic;
using System.Linq;
using System;

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
            if(hookInstances.Count < 1)
            {
                throw new ArgumentNullException("No Bot Instance was loaded. Please get one before start using WebHook function!");
            }
            if (name == null)
            {
                return hookInstances.First();
            }
            else
            {
                return hookInstances.Where(x => x.Name == name).First();
            }
        }

        public IList<WebHookInstance> HookInstances => hookInstances;
    }
}
