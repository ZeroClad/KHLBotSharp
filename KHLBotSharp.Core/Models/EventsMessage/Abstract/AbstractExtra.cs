using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Models.MessageHttps.EventMessage.Abstract
{
    public abstract class Extra
    {
        /// <summary>
        /// 消息种类
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public abstract class TextMessageExtra : Extra
    {
        /// <summary>
        /// 服务器ID
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        /// <summary>
        /// 频道名字
        /// </summary>
        [JsonProperty("channel_name")]
        public string ChannelName { get; set; }
        /// <summary>
        /// 艾特的人
        /// </summary>
        [JsonProperty("mention")]
        public IList<string> Mention { get; set; }
        /// <summary>
        /// 艾特全体成员
        /// </summary>
        [JsonProperty("mention_all")]
        public bool MentionAll { get; set; }
        /// <summary>
        /// 艾特的特定角色
        /// </summary>
        [JsonProperty("mention_roles")]
        public IList<long> MentionRoles { get; set; }
        /// <summary>
        /// 艾特在线的人
        /// </summary>
        [JsonProperty("mention_here")]
        public bool MentionHere { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        [JsonProperty("author")]
        public User Author { get; set; }
    }

    public abstract class AttachmentMessageExtra : Extra
    {
        /// <summary>
        /// 服务器ID
        /// </summary>
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        /// <summary>
        /// 不知道干啥的，反正文档有
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// 发送者ID
        /// </summary>

        [JsonProperty("author")]
        public User Author { get; set; }
    }

    /// <summary>
    /// 影片/视频详情
    /// </summary>
    public class VideoAttachment : Attachments
    {
        /// <summary>
        /// 影片大小
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; set; }
        /// <summary>
        /// 影片时长
        /// </summary>
        [JsonProperty("duration")]
        public double Duration { get; set; }
        /// <summary>
        /// 影片像素长
        /// </summary>
        [JsonProperty("width")]
        public float Width { get; set; }
        /// <summary>
        /// 影片像素高
        /// </summary>
        [JsonProperty("height")]
        public float Height { get; set; }
    }

    /// <summary>
    /// 图片文件资料
    /// </summary>
    public class PictureAttachments : Attachments
    {

    }

    /// <summary>
    /// 文件资料
    /// </summary>
    public class FileAttachment : Attachments
    {
        /// <summary>
        /// 文件大小
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; set; }
    }
}
