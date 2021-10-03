using Microsoft.AspNetCore.Mvc;
using KHLBotSharp.IService;
using System;
using System.Threading.Tasks;
using System.Text;
using KHLBotSharp.WebHook.NetCore3.Helper;
using Newtonsoft.Json.Linq;
using KHLBotSharp.WebHook.NetCore3.Services;
using KHLBotSharp.Core.Models.Config;

namespace KHLBotSharp.WebHook.NetCore3.Controllers
{
    public class HookController : Controller
    {
        private IPluginLoaderService pluginLoaderService;
        private ILogService logService;
        private IDecoderService decoderService;
        private IBotConfigSettings config;
        public HookController(IBotConfigSettings config, IPluginLoaderService pluginLoaderService, ILogService logService, IDecoderService decoderService, IServiceProvider provider)
        {
            this.pluginLoaderService = pluginLoaderService;
            this.pluginLoaderService.Init(provider);
            this.logService = logService;
            this.decoderService = decoderService;
            this.config = config;
        }

        [Route("{**catchAll}")]
        public IActionResult Fuck()
        {
            return RedirectPermanent("https://www.bilibili.com/video/BV1FZ4y1P7Wk/");
        }

        [HttpPost]
        [Route("/hook")]
        public IActionResult Index()
        {
            try
            {
                string json = HttpContext.Items[config.BotToken].ToString();
                if (string.IsNullOrEmpty(json) || string.IsNullOrWhiteSpace(json))
                {
                    return new EmptyResult();
                }
                JToken jtoken = JToken.Parse(json);
                var decoded = decoderService.DecodeEncrypt(jtoken);
                if(decoded == null)
                {
                    logService.Error("Invalid Json!");
                    return StatusCode(403);
                }
                var type = decoderService.GetEventType(decoded);
                //Check if token is correct
                if (!decoded.Value<JObject>("d").ContainsKey("verify_token") || decoded.Value<JObject>("d").Value<string>("verify_token") != config.VerifyToken)
                {
                    logService.Error("Invalid Token. Verification failed!" + decoded.Value<JObject>("d").Value<string>("verify_token"));
                    return StatusCode(403);
                }
                switch (type)
                {
                    case "Challenge":
                        System.IO.File.WriteAllText("test.json", jtoken.ToString());
                        var result = JObject.FromObject(new { challenge = decoded.Value<JObject>("d").Value<string>("challenge") }).ToString();
                        logService.Debug("Returning Result: "+result);
                        return Content(result, "application/json", Encoding.UTF8);
                    case "1":
                    case "2":
                    case "3":
                    case "9":
                    case "10":
                    case "255":
                        _ = decoded.ParseEvent(pluginLoaderService, config, logService);
                        break;
                    default:
                        return StatusCode(403);
                }
                return StatusCode(200);
            }
            catch(Exception ex)
            {
                logService.Error(ex.Message);
            }
            return StatusCode(403);
        }
    }
}
