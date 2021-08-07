using Microsoft.AspNetCore.Mvc;
using KHLBotSharp.IService;
using System.IO;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Buffers.Text;
using System.Text;
using KHLBotSharp.WebHook.NetCore3.Helper;

namespace KHLBotSharp.WebHook.NetCore3.Controllers
{
    public class HookController : Controller
    {
        private IPluginLoaderService pluginLoaderService;
        private IKHLHttpService khlHttpService;
        private ILogService logService;
        public HookController(IPluginLoaderService pluginLoaderService, IKHLHttpService khlHttpService, ILogService logService)
        {
            this.pluginLoaderService = pluginLoaderService;
            this.khlHttpService = khlHttpService;
            this.logService = logService;
        }

        [Route("/hook")]
        public async Task<JsonResult> Index()
        {
            try
            {
                string json = await  HttpContext.Request.Body.GetJson();
                logService.Info(json);
                System.IO.File.WriteAllText("test.json", json, Encoding.UTF8);
            }
            catch(Exception ex)
            {
                logService.Error(ex.Message);
                System.IO.File.WriteAllText("test.json", ex.ToString());
            }
            return Json(new {  });
        }
    }
}
