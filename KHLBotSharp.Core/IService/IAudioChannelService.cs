using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KHLBotSharp.Core.IService
{
    public interface IAudioChannelService
    {
        /// <summary>
        /// 解压开黑啦khl-voice，如果已经存在则自动跳过啥都不干，推荐在插件加载阶段直接运行
        /// </summary>
        /// <returns></returns>
        Task Init();
        /// <summary>
        /// 播放文件或者链接上的音频, 你也可以使用rtmp链接。注意Play功能会直接强制切换当前正在播放，因此如果有需要自己搞个列队还请自己搞
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task Play(string fileName, string channelId, bool repeat = false);
        /// <summary>
        /// 停止所有播放
        /// </summary>
        /// <returns></returns>
        Task Stop();
    }
}
