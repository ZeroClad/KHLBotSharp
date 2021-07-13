using KHLBotSharp.Common.Converter;
using KHLBotSharp.Models.MessageHttps.RequestMessage.Abstract;
using Newtonsoft.Json;
using System.ComponentModel;

namespace KHLBotSharp.Models.MessageHttps.RequestMessage
{
    public class GetServerMember:AbstractMessageType
    {
        public GetServerMember(string guild)
        {
            this.GuildId = guild;
        }
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; } = null;
        [JsonProperty("search")]
        public string Search { get; set; } = null;
        [JsonProperty("role_id")]
        public int? RoleId { get; set; } = null;
        [JsonProperty("mobile_verified")]
        [JsonConverter(typeof(BoolConverter))]
        public bool? IsMobileVerified { get; set; } = null;
        [JsonIgnore]
        public SortType SortByActiveTime { get; set; } = SortType.Undefined;
        [JsonProperty("active_time")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? ActiveTime
        {
            get
            {
                if(SortByActiveTime == SortType.Undefined)
                {
                    return null;
                }
                return (int)SortByActiveTime;
            }
            set
            {
                if(value == null)
                {
                    SortByActiveTime = SortType.Undefined;
                }
                SortByActiveTime = (SortType)value;
            }
        }
        [JsonIgnore]
        public SortType SortByJoinedTime{ get; set; } = SortType.Undefined;
        [JsonProperty("joined_at")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? JoinedAt
        {
            get
            {
                if (SortByJoinedTime == SortType.Undefined)
                {
                    return null;
                }
                return (int)SortByJoinedTime;
            }
            set
            {
                if (value == null)
                {
                    SortByJoinedTime = SortType.Undefined;
                }
                SortByJoinedTime = (SortType)value;
            }
        }
        [JsonProperty("page")]
        public int Page { get; set; } = 1;
        [JsonProperty("page_size")]
        public int PageSize { get; set; } = 100;
    }

    public enum SortType
    {
        Undefined = -1,
        Ascending = 0,
        Descending = 1
    }
}
