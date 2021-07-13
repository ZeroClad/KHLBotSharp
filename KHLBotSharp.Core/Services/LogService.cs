using KHLBotSharp.IService;
using Spectre.Console;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace KHLBotSharp.Services
{
    public class LogService : ILogService
    {
        private string botName;
        public void Debug(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")+ "]]: [/][grey54][[Dbg]]: [/][underline green1][[" + botName+ "]][/]: [underline cyan1][[" +callerName+ "]][/]: [white]" + log.Replace("[", "[[").Replace("]","]]") + "[/]");
        }

        public void Error(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][red][[Err]]: [/][underline green1][[" + botName + "]][/]: [underline cyan1][[" + callerName + "]][/]: [white]" + log.Replace("[", "[[").Replace("]", "]]") + "[/]");
        }

        public void Info(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][blue][[Inf]]: [/][underline green1][[" + botName + "]][/]: [underline cyan1][[" + callerName + "]][/]: [white]" + log.Replace("[", "[[").Replace("]", "]]") + "[/]");
        }

        public void Init(string bot)
        {
            this.botName = bot;
        }

        public void Warning(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][yellow][[Wrn]]: [/][underline green1][[" + botName + "]][/]: [underline cyan1][[" + callerName + "]][/]: [white]" + log.Replace("[", "[[").Replace("]", "]]") + "[/]");
        }
    }
}
