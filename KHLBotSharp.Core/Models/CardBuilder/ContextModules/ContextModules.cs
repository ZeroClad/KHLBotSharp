using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Core.Models
{
    public class ContextModules : ICardBodyComponent
    {
        public string Type => "context";
        [JsonProperty("elements")]
        public List<ICardContent> Elements { get; set; }
        public ContextModules AddElements(params ICardContent[] contents)
        {
            if(Elements == null)
            {
                Elements = new List<ICardContent>();
            }
            Elements.AddRange(contents);
            return this;
        }
    }
}
