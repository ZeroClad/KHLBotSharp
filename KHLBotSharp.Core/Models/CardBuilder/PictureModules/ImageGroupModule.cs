using Newtonsoft.Json;
using System.Collections.Generic;

namespace KHLBotSharp.Core.Models
{
    public class ImageGroupModule : ICardBodyComponent
    {
        public string Type => "image-group";
        [JsonProperty("elements")]
        public List<ICardImageComponent> Elements { get; set; }
        public ImageGroupModule AddElements(params ICardImageComponent[] imageComponents)
        {
            if(Elements == null)
            {
                Elements = new List<ICardImageComponent>();
            }
            Elements.AddRange(imageComponents);
            return this;
        }
    }
}
