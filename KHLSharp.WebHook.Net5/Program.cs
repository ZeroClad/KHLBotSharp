using KHLBotSharp.Core.BotHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace KHLBotSharp.WebHook.Net5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Welcome.Print();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
.ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<Startup>();
});
        }
    }
}
