using KHLBotSharp;
using KHLBotSharp.EventHandlers.TextEvents;
using KHLBotSharp.Models.EventsMessage;
using KHLBotSharp.Core.Models.Config;
using System;
using System.Threading.Tasks;
using KHLBotSharp.IService;
using KHLBotSharp.Models.MessageHttps.RequestMessage;

namespace TestPlugin
{
    /// <summary>
    /// 新版本插件写法，推荐用这个，在这里假设<seealso cref="IBotConfigSettings.ProcessChar"/>为"."
    /// </summary>
    public class NewPluginSample : IGroupTextMessageHandler, IAutoCommand
    {
        /// <summary>
        /// 在config.json内设置的<seealso cref="IBotConfigSettings.ProcessChar"/>加这个就是主要指令，因此根据假设将会是".测试"
        /// </summary>
        public string Name => "测试";
        /// <summary>
        /// 可以留Null, 用于复的指令使用，根据假设将会是".测"以及".Test"都可以运行这个插件
        /// </summary>
        public string[] Prefix => new string[] { "测", "Test" };
        /// <summary>
        /// 用于help指令，自动生成指令的解释介绍
        /// </summary>
        public string Description => "这是个测试的指令";
        /// <summary>
        /// 用于分类指令，可设置为null
        /// </summary>
        public string Group => "测试指令组";
        private ILogService logService;
        private IKHLHttpService requestFactory;
        private IBotConfigSettings botConfigSettings;
        /// <summary>
        /// For more info for this, view how we register DI in TestDI.cs!
        /// 更多详情，请查看TestDI.cs如何自定义注册Dependency Inject
        /// </summary>
        private ITestDI testDI;
        public Task Ctor(IServiceProvider provider)
        {
            logService = (ILogService)provider.GetService(typeof(ILogService));
            requestFactory = (IKHLHttpService)provider.GetService(typeof(IKHLHttpService));
            botConfigSettings = (IBotConfigSettings)provider.GetService(typeof(IBotConfigSettings));
            testDI = (ITestDI)provider.GetService(typeof(ITestDI));
            logService.Info("Loaded DI data");
            logService.Info("Testing config reading " + botConfigSettings.BotToken);
            return Task.CompletedTask;
        }

        public async Task<bool> Handle(EventMessage<GroupTextMessageEvent> eventArgs)
        {
            //因为使用了IAutoCommand因此无需再检测指令，直接运行功能即可，框架已经会自动过滤
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
            //我们使用已经注册过的DI
            testDI.HelloWorld();
            //停止后面插件的运行，表示这个指令我们已经完成了，后面的无需跟上
            return true;
        }
    }
}
