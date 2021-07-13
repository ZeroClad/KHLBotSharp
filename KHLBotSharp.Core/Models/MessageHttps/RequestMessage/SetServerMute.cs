using KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage
{
    public class SetServerMute:AbstractMessageType
    {
        public SetServerMute(string guildId, string userId, MuteType muteType)
        {
            GuildId = guildId;
            UserId = userId;
            MuteType = muteType;
        }
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("type")]
        public MuteType MuteType { get; set; }
    }

    public enum MuteType
    {
        Mic = 1,
        HeadSet = 2
    }
}
