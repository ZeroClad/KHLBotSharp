using KHLBotSharp;
using KHLBotSharp.IService;
using Microsoft.Extensions.DependencyInjection;

namespace TestPlugin
{
    public class PluginRegister : IServiceRegister
    {
        public void Register(IServiceCollection services)
        {
            services.AddScoped<ITestDI, TestDI>();
        }
    }

    public interface ITestDI
    {
        public void HelloWorld();
    }

    public class TestDI : ITestDI
    {
        private readonly ILogService log;
        public TestDI(ILogService logService)
        {
            log = logService;
        }

        public void HelloWorld()
        {
            log.Info("Hello World");
        }
    }
}
