using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KHLBotSharp.Core.Models
{
    public class Card : ICardComponent
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
                if (Enum.TryParse(ThemeString, true, out CardTheme theme))
                {
                    return theme;
                }
                ThemeString = "primary";
                return CardTheme.Primary;
            }
            set => ThemeString = Enum.GetName(typeof(CardTheme), value).ToLower();
        }

        [JsonProperty("modules")]
        public List<ICardBodyComponent> Modules { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonProperty("theme")]
        public string ThemeString { get; set; } = "primary";
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }
        public Card AddModules(params ICardBodyComponent[] cardComponents)
        {
            if (Modules == null)
            {
                Modules = new List<ICardBodyComponent>();
            }
            Modules.AddRange(cardComponents);
            return this;
        }

        public ActionGroupModule AddActionGroup()
        {
            var action = new ActionGroupModule();
            Modules.Add(action);
            return action;
        }

        public SectionModule AddSection()
        {
            var section = new SectionModule();
            Modules.Add(section);
            return section;
        }
    }

    public class CardBuilder : List<Card>
    {
        public Card Create()
        {
            var card = new Card();
            Add(card);
            return card;
        }

        public CardBuilder Create(params ICardBodyComponent[] components)
        {
            var card = new Card();
            card.AddModules(components);
            Add(card);
            return this;
        }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var json = JsonConvert.SerializeObject(this, Formatting.None, settings);
            return json;
        }
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
