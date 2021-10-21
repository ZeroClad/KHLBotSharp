using KHLBotSharp;
using KHLBotSharp.IService;
using Microsoft.Extensions.DependencyInjection;

namespace TestPlugin
{
    /// <summary>
    /// 创建注册DI的class
    /// </summary>
    public class PluginRegister : IServiceRegister
    {
        /// <summary>
        /// 可以在这里进行任意注册
        /// </summary>
        /// <param name="services"></param>
        public void Register(IServiceCollection services)
        {
            services.AddScoped<ITestDI, TestDI>();
        }
    }
    /// <summary>
    /// 需要被注册的interface
    /// </summary>
    public interface ITestDI
    {
        /// <summary>
        /// 测试功能
        /// </summary>
        public void HelloWorld();
    }
    /// <summary>
    /// 已实现注册interface的class
    /// </summary>
    public class TestDI : ITestDI
    {
        private readonly ILogService log;
        /// <summary>
        /// 可以获取到DI的数据等
        /// </summary>
        /// <param name="logService"></param>
        public TestDI(ILogService logService)
        {
            log = logService;
        }
        /// <summary>
        /// 测试功能
        /// </summary>
        public void HelloWorld()
        {
            log.Info("Hello World");
        }
    }
}
