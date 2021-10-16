using Microsoft.AspNetCore.Mvc;
using KHLBotSharp.IService;
using System;
using System.Text;
using KHLBotSharp.WebHook.Net5.Helper;
using Newtonsoft.Json.Linq;
using KHLBotSharp.Core.Models.Config;
using KHLBotSharp.Services;
using System.Threading.Tasks;

namespace KHLBotSharp.WebHook.Net5.Controllers
{
    public class HookController : Controller
    {
        private readonly IWebhookInstanceManagerService instanceManagerService;
        private readonly ILogService logService;
        public HookController(IWebhookInstanceManagerService webhookManager, ILogService logService)
        {
            instanceManagerService = webhookManager;
            this.logService = logService;
        }

        [Route("{**catchAll}")]
        public IActionResult Fuck()
        {
            logService.Warning(HttpContext.Connection.RemoteIpAddress.ToString() + " had accessed our bot illegally, lets send him to Tong Shen Serving Hot Pot");
            return RedirectPermanent("https://www.bilibili.com/video/BV1FZ4y1P7Wk/");
        }

        [HttpPost]
        [Route("/hook")]
        public async Task<IActionResult> Index(string botName = null)
        {
            try
            {
                var instance = instanceManagerService.Get(botName);
                var config = (IBotConfigSettings)instance.ServiceProvider.GetService(typeof(IBotConfigSettings));
                var pluginLoaderService = (IPluginLoaderService)instance.ServiceProvider.GetService(typeof(IPluginLoaderService));
                pluginLoaderService.Init(instance.ServiceProvider);
                var logService = (ILogService)instance.ServiceProvider.GetService(typeof(ILogService));
                var decoderService = (IDecoderService)instance.ServiceProvider.GetService(typeof(IDecoderService));
                string json = HttpContext.Items["Content"].ToString();
                if (string.IsNullOrEmpty(json) || string.IsNullOrWhiteSpace(json))
                {
                    return new EmptyResult();
                }
                JToken jtoken = JToken.Parse(json);
                var decoded = await decoderService.DecodeEncrypt(jtoken);
                if(decoded == null)
                {
                    logService.Error("Invalid Json!");
                    return StatusCode(403);
                }
                var type = await decoderService .GetEventType(decoded);
                //Check if token is correct
                if (!decoded.Value<JObject>("d").ContainsKey("verify_token") || decoded.Value<JObject>("d").Value<string>("verify_token") != config.VerifyToken)
                {
                    logService.Error("Invalid Token. Verification failed! Lets send him to Tong Shen Serving Hot Pot!");
                    return RedirectPermanent("https://www.bilibili.com/video/BV1FZ4y1P7Wk/");
                }
                switch (type)
                {
                    case "Challenge":
                        var result = JObject.FromObject(new { challenge = decoded.Value<JObject>("d").Value<string>("challenge") }).ToString();
                        logService.Info("Challenge resolved successfully");
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
                logService.Write(HttpContext.Items["Content"].ToString());
            }
            return StatusCode(403);
        }
    }
}
