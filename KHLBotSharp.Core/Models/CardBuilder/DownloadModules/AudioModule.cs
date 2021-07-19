namespace KHLBotSharp.Core.Models
{
    public class AudioModule : ICardBodyComponent
    {
        public string Type => "audio";
        public string Title { get; set; }
        public string Src { get; set; }
        public string Cover { get; set; }
    }
}
