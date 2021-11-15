using KHLBotSharp.EventHandlers.TextEvents;
using KHLBotSharp.IService;
using KHLBotSharp.Models.EventsMessage;
using System;
using System.Threading.Tasks;

namespace TestPlugin
{
    public class UploadFilePluginSample : IGroupTextMessageHandler
    {
        public string Name => "测试上传";

        public string[] Prefix => new string[] { "测上", "TA" };

        public string Description => "这个是用来测试上传文件的功能";

        public string Group => "测试指令组";

        private IKHLHttpService requestFactory;
        public Task Ctor(IServiceProvider provider)
        {
            requestFactory = (IKHLHttpService)provider.GetService(typeof(IKHLHttpService));
            return Task.CompletedTask;
        }

        public async Task<bool> Handle(EventMessage<GroupTextMessageEvent> eventArgs)
        {
            var url = await requestFactory.UploadFile("https://gxmcoc.xyz/Icon/coc-icon.png?width=100&height=100");
            await requestFactory.SendGroupMessage(eventArgs.CreateReply(url));
            return true;
        }
    }
}
