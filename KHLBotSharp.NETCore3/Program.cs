using KHLBotSharp.Host;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace KHLBotSharp.NETCore3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists("Profiles"))
            {
                Directory.CreateDirectory("Profiles");
                Directory.CreateDirectory("Profiles\\DefaultBot");
                Directory.CreateDirectory("Profiles\\DefaultBot\\Plugins");
                File.Copy("defaultConfig.json", Path.Combine(Environment.CurrentDirectory, "Profiles\\DefaultBot\\config.json"));
            }
            var bots = Directory.GetDirectories("Profiles");
            foreach (var bot in bots)
            {
                var config = JObject.Parse(File.ReadAllText(Path.Combine(bot, "config.json")));
                var token = config["BotToken"].ToString();
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("Missing token for " + bot.Split("\\").Last() + ". Skipping startup");
                    continue;
                }
                var botService = new BotService(token, bot);
                _ = botService.Run();
            }
            Console.WriteLine("All bots are loaded");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
