using KHLBotSharp.IService;
using System;
using System.Diagnostics;

namespace KHLBotSharp.Services
{
    /// <summary>
    /// 内置版本的IErrorRateService
    /// </summary>
    public class ErrorRateService : IErrorRateService
    {
        private int Errors = 0;
        private readonly int ErrorThreehold = 20;
        private int ResetError = 0;
        private readonly ILogService logService;
        public ErrorRateService(ILogService logService)
        {
            this.logService = logService;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void AddError()
        {
            Errors++;
            ResetError = 0;
            CheckRestart();
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void ReportStatus()
        {
            if (ResetError % 20 == 0)
            {
                logService.Debug("Detected Error Count: " + Errors);
            }
            CheckRestart();
        }
        private void CheckRestart()
        {
            if (Errors > ErrorThreehold)
            {
                logService.Error("Too much error detected, Restarting bot!");
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void CheckResetError()
        {
            if (ResetError > 15)
            {
                Errors = 0;
            }
            ResetError++;
        }
    }
}
