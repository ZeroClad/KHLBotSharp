using KHLBotSharp.Host;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        var config = JObject.Parse(File.ReadAllText(Path.Combine(bot, "config.json")));
        var token = config["BotToken"].ToString();
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Missing token for " + bot.Split("\\").Last() + ". Skipping startup");
        }
        var botService = new BotService(token, bot);
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
}

Console.WriteLine("All bots are loaded");
await Task.Delay(-1);