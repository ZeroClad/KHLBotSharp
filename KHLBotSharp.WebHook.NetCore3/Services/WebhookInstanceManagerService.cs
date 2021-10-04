using KHLBotSharp.WebHook.NetCore3.Models.Webhook;
using System.Collections.Generic;
using System.Linq;


namespace KHLBotSharp.WebHook.NetCore3.Services
{
    public class WebhookInstanceManagerService : IWebhookInstanceManagerService
    {
        private readonly IList<HookInstance> hookInstances = new List<HookInstance>();
        public IWebhookInstanceManagerService Add(HookInstance obj)
        {
            hookInstances.Add(obj);
            return this;
        }

        public HookInstance Get(string name)
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

        public IList<HookInstance> HookInstances
        {
            get
            {
                return hookInstances;
            }
        }
    }
}
