using KHLBotSharp.Core.Plugin;
using KHLBotSharp.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

                if (hostedService is BackgroundService backgroundService)
                {
                    _ = TryExecuteBackgroundServiceAsync(backgroundService);
                }
                AppDomain.CurrentDomain.ProcessExit += async (sender, e) => 
                { 
                    Task.WaitAll(hostedService.StopAsync()); 
                };
            }
        }


        //Copied from Microsoft https://github.com/dotnet/runtime/blob/cc7f2f9c1a80c262055757331acca25d2748d381/src/libraries/Microsoft.Extensions.Hosting/src/Internal/Host.cs
        private async Task TryExecuteBackgroundServiceAsync(BackgroundService backgroundService)
        {
            // backgroundService.ExecuteTask may not be set (e.g. if the derived class doesn't call base.StartAsync)
            Task backgroundTask = backgroundService.StartAsync(default);
            if (backgroundTask == null)
            {
                return;
            }

            try
            {
                await backgroundTask.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // When the host is being stopped, it cancels the background services.
                // This isn't an error condition, so don't log it as an error.
                if (backgroundTask.IsCanceled && ex is OperationCanceledException)
                {
                    return;
                }
                var logService = provider.GetService<ILogService>();
                logService.Error(ex.ToString());
            }
        }
    }
}
