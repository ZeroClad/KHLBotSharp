using KHLBotSharp.Core.BotHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace KHLBotSharp.WebHook.NetCore3
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
}).UseDefaultServiceProvider((context, options) =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});
        }
    }
}
