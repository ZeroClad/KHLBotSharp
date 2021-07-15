using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.EventHandlers.TextEvents;
using KHLBotSharp.IService;
using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Models.EventsMessage.Text;
using KHLBotSharp.Models.MessageHttps.RequestMessage;
using System;
using System.Threading.Tasks;

namespace TestPlugin
{
    public class Class1 : IGroupTextMessageHandler
    {
        private ILogService logService;
        private IKHLHttpService requestFactory;
        private IBotConfigSettings botConfigSettings;
        public Task Ctor(IServiceProvider provider)
        {
            logService = (ILogService)provider.GetService(typeof(ILogService));
            requestFactory = (IKHLHttpService)provider.GetService(typeof(IKHLHttpService));
            botConfigSettings = (IBotConfigSettings)provider.GetService(typeof(IBotConfigSettings));
            logService.Info("Loaded DI data");
            logService.Info("Testing config reading " + botConfigSettings.BotToken);
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
