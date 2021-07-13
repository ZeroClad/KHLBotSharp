using System;
using System.Runtime.CompilerServices;

namespace KHLBotSharp.IService
{
    public interface ILogService
    {
        [Obsolete]
        void Init(string bot);
        void Info(string log, [CallerFilePath] string callerName = "");
        void Warning(string log, [CallerFilePath] string callerName = "");
        void Error(string log, [CallerFilePath] string callerName = "");
        void Debug(string log, [CallerFilePath] string callerName = "");
    }
}
