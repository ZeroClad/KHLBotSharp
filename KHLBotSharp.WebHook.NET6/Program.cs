using KHLBotSharp.Core.BotHost;

namespace KHLBotSharp.WebHook.NET6
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Welcome.Print();
            await CreateHostBuilder(args).Build().RunBot();
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
