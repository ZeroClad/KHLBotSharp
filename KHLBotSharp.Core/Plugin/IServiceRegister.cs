using Microsoft.Extensions.DependencyInjection;

namespace KHLBotSharp
{
    public interface IServiceRegister
    {
        /// <summary>
        /// 在这里注册DI
        /// 注意！别TM在这里注册你的插件！！而是注册你插件需要用到的Service!!
        /// </summary>
        /// <param name="services"></param>
        void Register(IServiceCollection services);
    }
}
