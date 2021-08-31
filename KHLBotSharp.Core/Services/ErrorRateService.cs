using KHLBotSharp.IService;
using System;
using System.Diagnostics;

namespace KHLBotSharp.Services
{
    class ErrorRateService : IErrorRateService
    {
        private int Errors = 0;
        private int ErrorThreehold = 20;
        private int ResetError = 0;
        private ILogService logService;
        public ErrorRateService(ILogService logService)
        {
            this.logService = logService;
        }

        public void AddError()
        {
            Errors++;
            ResetError = 0;
        }

        public void ReportStatus()
        {
            if(ResetError % 5 == 0)
            {
                logService.Debug("Detected Error Count: " + Errors);
            }
            if(Errors > ErrorThreehold)
            {
                logService.Error("Too much error detected, Restarting bot!");
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Environment.Exit(0);
            }
        }

        public void CheckResetError()
        {
            if(ResetError > 10)
            {
                Errors = 0;
            }
            ResetError++;
        }
    }
}
