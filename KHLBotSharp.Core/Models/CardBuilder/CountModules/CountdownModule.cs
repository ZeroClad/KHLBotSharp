using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace KHLBotSharp.Core.Models
{
    public class CountdownModule : ICardBodyComponent
    {
        public string Type => "countdown";
        public long EndTime { get; set; }
        public long StartTime { get; set; }
        [JsonIgnore]
        public CountMode Mode
        {
            get
            {
                if (Enum.TryParse(ModeString, true, out CountMode mode))
                {
                    return mode;
                }
                ModeString = "hour";
                return CountMode.Hour;
            }
            set
            {
                ModeString = Enum.GetName(typeof(CountMode), value).ToLower();
            }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonProperty("theme")]
        public string ModeString { get; set; }
    }

    public enum CountMode
    {
        Day,
        Hour,
        Second
    }
}
