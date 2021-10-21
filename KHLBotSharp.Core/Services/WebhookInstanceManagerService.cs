using KHLBotSharp.Core.BotHost;
using System;
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
            if (hookInstances.Count < 1)
            {
                throw new ArgumentException("No Bot Instance was loaded. Please get one before start using WebHook function!");
            }
            if (name == null)
            {
                return hookInstances.First();
            }
            else
            {
                var botList = hookInstances.Where(x => x.Name == name);
                if (botList.Count() < 1)
                {
                    throw new ArgumentException("No Bot Instance named as " + name + " was found! Did you disabled the bot?");
                }
                return botList.First();
            }
        }

        public IList<WebHookInstance> HookInstances => hookInstances;
    }
}
