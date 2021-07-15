using KHLBotSharp.Core.Host;
using KHLBotSharp.Host;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

Welcome.Print();
if(args.Length > 1)
{
    var profileName = args[1];
    if (args[0] == "-c")
    {
        if (!Directory.Exists("Profiles"))
        {
            Directory.CreateDirectory("Profiles");
        }
        if (!Directory.Exists("Profiles\\" + profileName))
        {
            Directory.CreateDirectory("Profiles\\" + profileName);
            Directory.CreateDirectory("Profiles\\" + profileName + "\\Plugins");
            File.Copy("defaultConfig.json", Path.Combine(Environment.CurrentDirectory, "Profiles\\" + profileName + "\\config.json"));
        }
        Console.WriteLine("Profile creation success!");
        return;
    }
    else if(args[0] == "-r")
    {
        if (!Directory.Exists("Profiles"))
        {
            Directory.CreateDirectory("Profiles");
        }
        if (!Directory.Exists("Profiles\\" + profileName))
        {
            Directory.CreateDirectory("Profiles\\" + profileName);
            Directory.CreateDirectory("Profiles\\" + profileName + "\\Plugins");
            File.Copy("defaultConfig.json", Path.Combine(Environment.CurrentDirectory, "Profiles\\" + profileName + "\\config.json"));
        }
        var bot = "Profiles\\" + profileName;
        var botService = new BotService(bot);
        _ = botService.Run();
    }
}
else
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
        var botService = new BotService(bot);
        _ = botService.Run();
    }
}
await Task.Delay(-1);