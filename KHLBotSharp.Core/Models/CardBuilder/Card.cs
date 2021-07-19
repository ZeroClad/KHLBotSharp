using KHLBotSharp.Core.Common.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KHLBotSharp.Core.Models
{
    public class Card:ICardComponent
    {
        [JsonProperty("type")]
        public string Type { get; } = "card";
        [JsonProperty("size")]
        public string Size { get; set; } = "sm";
        [JsonIgnore]
        public CardTheme Theme
        {
            get
            {
                if(Enum.TryParse(ThemeString, out CardTheme theme))
                {
                    return theme;
                }
                return CardTheme.Primary;
            }
            set
            {
                ThemeString = Enum.GetName(typeof(CardTheme), value);
            }
        }

        [JsonProperty("modules")]
        public List<ICardBodyComponent> Modules { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonProperty("theme")]
        public string ThemeString { get; set; }

        public ICardComponent AddModules(params ICardBodyComponent[] cardComponents)
        {
            Modules.AddRange(cardComponents);
            return this;
        }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json = JsonConvert.SerializeObject(this, Formatting.Indented, settings);
            return json;
        }
    }

    public class CardBuilder: List<Card>
    {

    }

    public enum CardTheme
    {
        Primary,
        Secondary,
        Info,
        Success,
        Warning,
        Danger
    }
}
