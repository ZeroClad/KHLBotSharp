using KHLBotSharp.IService;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace KHLBotSharp.Services
{
    public class LogService : ILogService
    {
        private string botName;
        private string logColor;
        private readonly List<string> colorCodes = new List<string>
        {
            "fuchsia",
            "green3",
            "purple_1",
            "lightcyan1",
            "plum1",
            "wheat1",
            "white"
        };
        public void Debug(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")+ "]]: [/][grey54][[Dbg]]: [/][underline green1][[" + botName+ "]][/]: [underline cyan1][[" +callerName+ "]][/]: ["+logColor+"]" + log.Replace("[", "[[").Replace("]","]]") + "[/]");
        }

        public void Error(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][red][[Err]]: [/][underline green1][[" + botName + "]][/]: [underline cyan1][[" + callerName + "]][/]: ["+logColor+"]" + log.Replace("[", "[[").Replace("]", "]]") + "[/]");
        }

        public void Info(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][blue][[Inf]]: [/][underline green1][[" + botName + "]][/]: [underline cyan1][[" + callerName + "]][/]: ["+logColor+"]" + log.Replace("[", "[[").Replace("]", "]]") + "[/]");
        }

        public void Init(string bot)
        {
            this.botName = bot;
            Random rnd = new Random();
            var selectColor = rnd.Next(0, colorCodes.Count);
            logColor = colorCodes[selectColor];
        }

        public void Warning(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][yellow][[Wrn]]: [/][underline green1][[" + botName + "]][/]: [underline cyan1][[" + callerName + "]][/]: ["+logColor+"]" + log.Replace("[", "[[").Replace("]", "]]") + "[/]");
        }
    }
}
