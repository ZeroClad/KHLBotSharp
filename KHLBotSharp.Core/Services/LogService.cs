using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace KHLBotSharp.Services
{
    public class LogService : ILogService
    {
        private string botName;
        private string logColor;
        private bool InitState, showDebug;

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
            if (!showDebug)
            {
                return;
            }
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")+ "]]: [/][grey54][[Dbg]]: [/][underline green1][[" + botName+ "]][/]: [underline cyan1][[" +callerName+ "]][/]: ["+logColor+"]" + log.Replace("[", "[[").Replace("]","]]") + "[/]");
            WriteFile("[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "]: [Dbg]: " + log);
        }

        public void Error(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][red][[Err]]: [/][underline green1][[" + botName + "]][/]: [underline cyan1][[" + callerName + "]][/]: ["+logColor+"]" + log.Replace("[", "[[").Replace("]", "]]") + "[/]");
            WriteFile("[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "]: [Err]: " + log);
        }

        public void Info(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][blue][[Inf]]: [/][underline green1][[" + botName + "]][/]: [underline cyan1][[" + callerName + "]][/]: ["+logColor+"]" + log.Replace("[", "[[").Replace("]", "]]") + "[/]");
            WriteFile("[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "]: [Inf]: " + log);
        }

        public void Init(string bot, IBotConfigSettings configSettings)
        {
            if (InitState)
            {
                throw new InvalidOperationException("Do not init in plugins! You son of the bxtch");
            }
            InitState = true;
            this.botName = bot;
            Random rnd = new Random();
            var selectColor = rnd.Next(0, colorCodes.Count);
            logColor = colorCodes[selectColor];
            showDebug = configSettings.Debug;
        }

        private void WriteFile(string log)
        {
            var fileName = DateTime.Now.ToString("yyyy_MM_dd") + ".log";
            var path = Path.Combine(Environment.CurrentDirectory, "Profiles", botName, "Log");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var logPath = Path.Combine(path, fileName);
            using (StreamWriter stream = File.AppendText(logPath))
            {
                stream.WriteLine(log);
            }
        }

        public void Warning(string log, [CallerFilePath] string callerName = "")
        {
            callerName = callerName.Split('\\').Last().Replace(".cs", "");
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][yellow][[Wrn]]: [/][underline green1][[" + botName + "]][/]: [underline cyan1][[" + callerName + "]][/]: ["+logColor+"]" + log.Replace("[", "[[").Replace("]", "]]") + "[/]");
            WriteFile("[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "]: [Wrn]: " + log);
        }
    }
}
