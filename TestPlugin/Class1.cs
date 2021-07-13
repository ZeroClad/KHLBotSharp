using KHLBotSharp.Common.Request;
using KHLBotSharp.EventHandlers.TextEvents;
using KHLBotSharp.IService;
using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.EventsMessage.Text;
using KHLBotSharp.Models.MessageHttps.RequestMessage;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace TestPlugin
{
    public class Class1 : IGroupTextMessageHandler
    {
        private ILogService logService;
        private IRequestFactory requestFactory;
        public Task Ctor(IServiceProvider provider)
        {
            logService = (ILogService)provider.GetService(typeof(ILogService));
            requestFactory = (IRequestFactory)provider.GetService(typeof(IRequestFactory));
            logService.Info("Loaded DI data");
            return Task.CompletedTask;
        }

        public async Task<bool> Handle(EventMessage<GroupTextMessageEvent> eventArgs)
        {
            /*
            RestRequest rest = new RestRequest();
            RestClient restClient = new RestClient();
            restClient.BaseUrl = new Uri("https://gxmcoc.xyz");
            await restClient.ExecuteGetAsync(rest);
            */
            logService.Info("复读机运行中");
            await requestFactory.SendGroupMessage(new SendMessage(eventArgs.Data, eventArgs.Data.Content));
            return true;
        }
    }
}
