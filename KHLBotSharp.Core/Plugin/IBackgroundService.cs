using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KHLBotSharp.Core.Plugin
{
    /// <summary>
    /// 在机器人启动时同时运行的Service<br/>
    /// 可在这里创建Timer或者后台运行的系统
    /// </summary>
    public interface IBackgroundService
    {
        Task StartAsync();
        Task StopAsync();
    }
}
