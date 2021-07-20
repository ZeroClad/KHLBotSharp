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
    //演示KHLBotSharp的插件编写方法
    public class PluginSample : IGroupTextMessageHandler
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
            //示范可以自主使用第三方Nuget运行插件
            /*
            RestRequest rest = new RestRequest();
            RestClient restClient = new RestClient();
            restClient.BaseUrl = new Uri("https://gxmcoc.xyz");
            await restClient.ExecuteGetAsync(rest);
            */
            //*********************************************************
            //示范复读机
            /*
            logService.Info("复读机运行中");
            await requestFactory.SendGroupMessage(new SendMessage(eventArgs.Data, eventArgs.Data.Content));
            */
            //*********************************************************
            //示范KMarkdown复读机
            /*
            logService.Info("复读机运行中");
            await requestFactory.SendGroupMessage(new SendMessage(eventArgs.Data, eventArgs.Data.Content){ Type = 9 });
            */
            //*********************************************************
            //示范KMarkdown复读机，私聊消息
            /*
            logService.Info("复读机运行中");
            await requestFactory.SendGroupMessage(new SendMessage(eventArgs.Data, eventArgs.Data.Content, false, true){ Type = 9 });
            */
            //*********************************************************
            //示范Card消息，SendMessage会自动检测Json后进行切换成为Card消息因此无需手动输入Type, false为无需回复指令消息，正常默认都会自动回复
            await requestFactory.SendGroupMessage(new SendMessage(eventArgs.Data, CardBuilderSample.GetCard(), false));
            //停止后面插件的运行，表示这个指令我们已经完成了，后面的无需跟上
            return true;
        }
    }
}
