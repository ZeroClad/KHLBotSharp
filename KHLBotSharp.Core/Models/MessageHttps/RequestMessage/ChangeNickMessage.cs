using KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract;
using Newtonsoft.Json;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage
{
    public class ChangeNickMessage : AbstractMessageType
    {
        public ChangeNickMessage(string guild, string newnick, string user_id)
        {
            GuildId = guild;
            NickName = newnick;
            UserId = user_id;
        }
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
