using System;
using System.Runtime.CompilerServices;

namespace KHLBotSharp.IService
{
    public interface ILogService
    {
        [Obsolete]
        public void Init(string bot);
        public void Info(string log, [CallerFilePath] string callerName = "");
        public void Warning(string log, [CallerFilePath] string callerName = "");
        public void Error(string log, [CallerFilePath] string callerName = "");
        public void Debug(string log, [CallerFilePath] string callerName = "");
    }
}
