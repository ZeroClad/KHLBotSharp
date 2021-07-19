using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Core.Models
{
    public class ActionGroupModule : ICardBodyComponent
    {
        public string Type => "action-group";
        [JsonProperty("elements")]
        public List<ICardButtonComponent> Elements { get; set; }
        public ActionGroupModule AddElements(params ICardButtonComponent[] buttonComponents)
        {
            if(Elements == null)
            {
                Elements = new List<ICardButtonComponent>();
            }
            Elements.AddRange(buttonComponents);
            return this;
        }
    }
}
