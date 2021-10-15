using Spectre.Console;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace KHLBotSharp.Core.BotHost
{
    /// <summary>
    /// 欢迎词汇输出用，内置自动取消Console的快速选择避免出现Bot被选择相关文字后停止运行以及错误重启，不推荐Webhook使用
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Welcome
    {
        private const uint ENABLE_QUICK_EDIT = 0x0040;

        // STD_INPUT_HANDLE (DWORD): -10 is the standard input device.
        private const int STD_INPUT_HANDLE = -10;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        /// <summary>
        /// 输出欢迎以及绑定错误重启以及取消Console快速选择
        /// </summary>
        public static void Print()
        {
            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            // get current console mode
            if (!GetConsoleMode(consoleHandle, out uint consoleMode))
            {
                // ERROR: Unable to get console mode.
                // However who cares?
            }

            // Clear the quick edit bit in the mode flags
            consoleMode &= ~ENABLE_QUICK_EDIT;

            // set the new mode
            if (!SetConsoleMode(consoleHandle, consoleMode))
            {
                // ERROR: Unable to set console mode
                // However who cares?
            }

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AnsiConsole.Write(new FigletText("KHLBot v" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion).Centered().Color(Color.Aqua));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][red][[Err]]: [/][underline green1][[Global]][/]: [underline cyan1][[BotService]][/]: [white]" + e.ToString().Replace("[", "[[").Replace("]", "]]") + "[/]");
            File.WriteAllText("error.log", e.ExceptionObject.ToString());
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Environment.Exit(0);
            //Exit and restart
        }
    }
}
