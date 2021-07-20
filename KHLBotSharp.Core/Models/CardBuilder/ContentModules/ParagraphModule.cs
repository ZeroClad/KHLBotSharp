using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KHLBotSharp.Core.Models
{
    public class ParagraphModule : ICardParagraphComponent
    {
        public string Type => "paragraph";
        [JsonProperty("cols")]
        public decimal Cols { get; set; }
        [JsonProperty("fields")]
        public List<ICardTextComponent> Fields { get; set; }

        public ParagraphModule AddFields(params ICardTextComponent[] textComponents)
        {
            if(Fields == null)
            {
                Fields = new List<ICardTextComponent>();
            }
            if(Cols > 3)
            {
                throw new ArgumentException("Cols cannot larger than 3");
            }
            if(textComponents.Length > Cols)
            {
                throw new ArgumentException("Components is more than default cols! Please set it before add fields or make sure your components are not more than "+Cols);
            }
            Fields.AddRange(textComponents);
            return this;
        }
    }
}
