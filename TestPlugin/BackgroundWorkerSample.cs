using KHLBotSharp.Core.Plugin;
using KHLBotSharp.IService;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace TestPlugin
{
    //Similar to IHostingService however since the service won't run in ASP so we use our own one
    public class BackgroundWorkerSample : IBackgroundService
    {
        private ILogService log;
        public BackgroundWorkerSample(ILogService log)
        {
            this.log = log;
        }

        public Task StartAsync()
        {
            log.Info("Background Worker Started");
            Timer timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();
            return Task.CompletedTask;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            log.Info("Ping from Background worker");
        }

        public Task StopAsync()
        {
            File.WriteAllText("save.txt", "Background worker exit safely");
            return Task.CompletedTask;
        }
    }
}
