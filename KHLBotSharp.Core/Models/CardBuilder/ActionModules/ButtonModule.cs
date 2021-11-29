using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace KHLBotSharp.Core.Models
{
    public class ButtonModule : ICardButtonComponent
    {
        public string Type => "button";
        [JsonIgnore]
        public CardTheme Theme
        {
            get
            {
                if (Enum.TryParse(ThemeString, true, out CardTheme theme))
                {
                    return theme;
                }
                ThemeString = "primary";
                return CardTheme.Primary;
            }
            set => ThemeString = Enum.GetName(typeof(CardTheme), value).ToLower();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonProperty("theme")]
        public string ThemeString { get; set; } = "primary";
        [JsonProperty("value")]
        public string Value { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("click")]
        public string Click { get; set; } = "return-val";
        public ICardTextComponent Text { get; set; }
    }
}
