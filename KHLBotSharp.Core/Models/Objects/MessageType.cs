﻿namespace KHLBotSharp.Core.Models.Objects
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType : int
    {
        TextMessage = 9,
        ImageMessage = 2,
        VideoMessage = 3,
        FileMessage = 4,
        AudioMessage = 8,
        KMarkdownMessage = 9,
        CardMessage = 10,
        SystemMessage = 255
    }
}
