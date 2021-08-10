using Microsoft.AspNetCore.Mvc;
using KHLBotSharp.IService;
using System;
using System.Threading.Tasks;
using System.Text;
using KHLBotSharp.WebHook.NetCore3.Helper;
using Newtonsoft.Json.Linq;
using KHLBotSharp.WebHook.NetCore3.Services;
using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.Models.EventsMessage;

namespace KHLBotSharp.WebHook.NetCore3.Controllers
{
    public class HookController : Controller
    {
        private IPluginLoaderService pluginLoaderService;
        private IKHLHttpService khlHttpService;
        private ILogService logService;
        private IDecoderService decoderService;
        private IBotConfigSettings config;
        private IServiceProvider provider;
        public HookController(IBotConfigSettings config, IPluginLoaderService pluginLoaderService, IKHLHttpService khlHttpService, ILogService logService, IDecoderService decoderService, IServiceProvider provider)
        {
            this.pluginLoaderService = pluginLoaderService;
            this.khlHttpService = khlHttpService;
            this.logService = logService;
            this.decoderService = decoderService;
            this.config = config;
            this.provider = provider;
        }

        [Route("{**catchAll}")]
        public IActionResult Fuck()
        {
            return RedirectPermanent("https://www.bilibili.com/video/BV1FZ4y1P7Wk/");
        }

        [HttpPost]
        [Route("/hook")]
        public async Task<IActionResult> Index(int Compress = 1)
        {
            if(Compress != 0)
            {
                return Json(new { Error = "Please disable compress to use this or wait for the damn lazy dev to add this decoding function" });
            }
            try
            {
                string json = await HttpContext.Request.Body.GetJson();
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
                        _ = decoded.ParseEvent(pluginLoaderService, config, logService, provider);
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
