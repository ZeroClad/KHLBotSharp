using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KHLBotSharp.Core.Models
{
    public class ContainerModule : ICardBodyComponent
    {
        public string Type => "container";
        [JsonProperty("elements")]
        public List<ICardImageComponent> Elements { get; set; }
        public ContainerModule AddElements(params ICardImageComponent[] imageComponents)
        {
            if (Elements == null)
            {
                Elements = new List<ICardImageComponent>();
            }
            Elements.AddRange(imageComponents);
            return this;
        }
    }
}
