using Newtonsoft.Json;
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
            Fields.AddRange(textComponents);
            return this;
        }
    }
}
