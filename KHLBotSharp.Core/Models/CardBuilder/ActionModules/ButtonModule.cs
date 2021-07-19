using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KHLBotSharp.Core.Models
{
    public class ButtonModule : ICardButtonComponent
    {
        public string Type => "button";
        [JsonProperty("theme")]
        public CardTheme Theme { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        public ICardTextComponent Text { get; set; }
    }
}
