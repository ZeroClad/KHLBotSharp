using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using KHLBotSharp.Common.MessageParser;
using KHLBotSharp.Core.BotHost;
using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.IService;
using KHLBotSharp.Services;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KHLBotSharp.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmark>();
        }
    }

    public class Benchmark
    {
        public BotService bot { get; set; }
        [Benchmark]
        public async Task CreateInstance()
        {
            if (!Directory.Exists("Profiles"))
            {
                Directory.CreateDirectory("Profiles");
                Directory.CreateDirectory("Profiles\\Benchmark");
                Directory.CreateDirectory("Profiles\\Benchmark\\Plugins");
            }
            bot = new BotService("Profiles\\Benchmark");
            var log = (ILogService)bot.provider.GetService(typeof(ILogService));
            var config = (IBotConfigSettings)bot.provider.GetService(typeof(IBotConfigSettings));
            var pluginLoader = (IPluginLoaderService)bot.provider.GetService(typeof(IPluginLoaderService));
            config.BotToken = "1/MTAzNjA=/TokrEHBs01OaTe/+BiXK3w==";
            config.EncryptKey = "GeybRF";
            config.VerifyToken = "I-SnOIxmWrFQjemQ";
            var token = JToken.Parse("{\"encrypt\":\"ZDQ0ODkwNmU3OTY4Yzc3NG9WU1dMcFliSUhUQXgwd3FGbzZJaHJ1b1VPRTBvKzFPeXBhTExjVUhjdEZhL2lXY3p6cEJ0Mm8yQWdUK0N6dUx4Ym01eVJxUDBMelVCeDFTUllCaWRrRzZ5QXBCV2FPeVhoejl4UlllMjNjS3hHbDVPcTQ1TVB2YzZoUEtiL1pMV0tSUVJUWnRTK0pGUUtvZjJPYlMxVjNoL0VraGxnRUhCOTJmNWZ1M05UUT0=\"}");
            var decoder = new DecoderService(config, log);
            await decoder.DecodeEncrypt(token);
        }
    }
}
