using Newtonsoft.Json;
using System.ComponentModel;

namespace KHLBotSharp.Core.Models
{
    public class ImageModule : ICardImageComponent
    {
        public string Type => "image";
        [JsonProperty("src")]
        public string Src { get; set; }
        [JsonIgnore]
        public Size Size { get; set; }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonProperty("Size")]
        public string _Size
        {
            get
            {
                return Size.ToString().ToLower();
            }
        }
    }

    public enum Size
    {
        SM,
        LG
    }
}
