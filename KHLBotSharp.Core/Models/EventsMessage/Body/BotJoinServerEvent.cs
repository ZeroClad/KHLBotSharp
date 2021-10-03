using KHLBotSharp.Models.EventsMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.EventsMessage
{
    /// <summary>
    /// 机器人加入服务器
    /// </summary>
    public class BotJoinServerEvent : AbstractBody
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
