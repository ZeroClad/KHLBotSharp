using KHLBotSharp.Core.Plugin;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KHLBotSharp.Core.BotHost
{
    public class BackgroundServiceRunner
    {
        private readonly IServiceProvider provider;
        public BackgroundServiceRunner(IServiceProvider provider)
        {
            this.provider = provider;
        }
        //Create background host here since we don't able to run such thing in WebSocket version

        public async void RunIHostServices()
        {
            var bgservices = provider.GetServices<IBackgroundService>();
            foreach (IBackgroundService hostedService in bgservices)
            {
                // Fire IHostedService.Start
                await hostedService.StartAsync().ConfigureAwait(false);
                AppDomain.CurrentDomain.ProcessExit += (sender, e) => 
                { 
                    Task.WaitAll(hostedService.StopAsync()); 
                };
            }
        }
    }
}
