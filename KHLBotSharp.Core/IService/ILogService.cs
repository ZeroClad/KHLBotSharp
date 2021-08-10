using KHLBotSharp.Core.Models.Config;
using System;
using System.Runtime.CompilerServices;

namespace KHLBotSharp.IService
{
    public interface ILogService
    {
        [Obsolete]
        void Init(string bot, IBotConfigSettings configSettings);
        /// <summary>
        /// Write Info Log on file and console
        /// </summary>
        /// <param name="log"></param>
        /// <param name="callerName"></param>
        void Info(string log, [CallerFilePath] string callerName = "");
        /// <summary>
        /// Write Warning Log on file and console
        /// </summary>
        /// <param name="log"></param>
        /// <param name="callerName"></param>
        void Warning(string log, [CallerFilePath] string callerName = "");
        /// <summary>
        /// Write Error Log on file and console
        /// </summary>
        /// <param name="log"></param>
        /// <param name="callerName"></param>
        void Error(string log, [CallerFilePath] string callerName = "");
        /// <summary>
        /// Write Debug Log on file and console
        /// </summary>
        /// <param name="log"></param>
        /// <param name="callerName"></param>
        void Debug(string log, [CallerFilePath] string callerName = "");
        /// <summary>
        /// Write Log on file only
        /// </summary>
        /// <param name="log"></param>
        /// <param name="callerName"></param>
        void Write(string log, [CallerFilePath] string callerName = "");
    }
}
