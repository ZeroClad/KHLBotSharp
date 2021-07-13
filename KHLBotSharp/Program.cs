using KHLBotSharp.Host;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;

if (!Directory.Exists("Profiles"))
{
    Directory.CreateDirectory("Profiles");
    Directory.CreateDirectory("Profiles\\DefaultBot");
    Directory.CreateDirectory("Profiles\\DefaultBot\\Plugins");
    File.Copy("defaultConfig.json", "Profiles\\DefaultBot\\config.json");
}
var bots = Directory.GetDirectories("Profiles");
foreach (var bot in bots)
{
    var config = JObject.Parse(File.ReadAllText(Path.Combine(bot, "config.json")));
    var botService = new BotService(config["BotToken"].ToString(), bot);
    _ = botService.Run();
}
await Task.Delay(-1);