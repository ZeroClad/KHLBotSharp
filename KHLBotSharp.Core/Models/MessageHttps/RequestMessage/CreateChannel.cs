using KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract;
using KHLBotSharp.Models.Objects;
using Newtonsoft.Json;
using System.ComponentModel;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage
{
    public class CreateChannel : AbstractMessageType
    {
        public CreateChannel(string guildId, string name)
        {
            GuildId = guildId;
            Name = name;
        }
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public ChannelType Type { get; set; }
        [JsonProperty("limit_amount")]
        public int? LimitAmount { get; set; }
        [JsonProperty("voice_quality")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? Voice { get; set; }
        [JsonIgnore]
        public VoiceQuality VoiceQuality
        {
            get
            {
                if(Voice == null)
                {
                    return VoiceQuality.Undefined;
                }
                return (VoiceQuality)Voice.Value;
            }
            set
            {
                if(value == VoiceQuality.Undefined)
                {
                    Voice = null;
                }
                Voice = (int)value;
            }
        }
    }

    public enum VoiceQuality
    {
        Undefined,
        Smooth,
        Normal,
        HighQuality
    }
}
