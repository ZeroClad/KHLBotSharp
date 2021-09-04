using Microsoft.Extensions.DependencyInjection;

namespace KHLBotSharp
{
    public interface IServiceRegister
    {
        /// <summary>
        /// Register DI here. Please do not register any kind of IPlugin! We will handle it automatically! 
        /// WARNING! Register what you need IN your plugin, NOT YOUR PLUGIN ITSELF!
        /// </summary>
        /// <param name="services"></param>
        void Register(IServiceCollection services);
    }
}
