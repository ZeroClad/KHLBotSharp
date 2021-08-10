using Spectre.Console;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace KHLBotSharp.Core.BotHost
{
    public class Welcome
    {
        const uint ENABLE_QUICK_EDIT = 0x0040;

        // STD_INPUT_HANDLE (DWORD): -10 is the standard input device.
        const int STD_INPUT_HANDLE = -10;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        public static void Print()
        {
            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            // get current console mode
            uint consoleMode;
            if (!GetConsoleMode(consoleHandle, out consoleMode))
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
            AnsiConsole.Render(new FigletText("KHLBot v" + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion).Centered().Color(Color.Aqua));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            AnsiConsole.MarkupLine("[grey42][[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]]: [/][red][[Err]]: [/][underline green1][[Global]][/]: [underline cyan1][[BotService]][/]: [white]" + e.ToString().Replace("[", "[[").Replace("]", "]]") + "[/]");
            File.WriteAllText("error.log", e.ToString());
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Environment.Exit(0);
            //Exit and restart
        }
    }
}
