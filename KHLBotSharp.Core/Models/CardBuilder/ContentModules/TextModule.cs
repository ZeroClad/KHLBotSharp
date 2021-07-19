using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace KHLBotSharp.Core.Models
{
    public class TextModule : ICardTextComponent
    {
        [JsonProperty("type")]
        public string Type => "plain-text";
        [JsonProperty("content")]
        public string Content { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ICardComponent AddModules(params ICardComponent[] cardComponents)
        {
            throw new ArgumentException("Text Module can't add new modules!");
        }
    }
}
