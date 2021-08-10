using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Core.Models
{
    public class ContextModule : ICardBodyComponent
    {
        public string Type => "context";
        [JsonProperty("elements")]
        public List<ICardContent> Elements { get; set; }
        public ContextModule AddElements(params ICardContent[] contents)
        {
            if (Elements == null)
            {
                Elements = new List<ICardContent>();
            }
            Elements.AddRange(contents);
            return this;
        }
    }
}
