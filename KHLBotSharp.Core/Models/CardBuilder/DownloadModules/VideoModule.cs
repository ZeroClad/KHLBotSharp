namespace KHLBotSharp.Core.Models
{
    public class VideoModule : ICardBodyComponent
    {
        public string Type => "video";
        public string Title { get; set; }
        public string Src { get; set; }
    }
}
