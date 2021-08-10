namespace KHLBotSharp.Core.Models
{
    public class FileModule : ICardBodyComponent
    {
        public string Type => "file";
        public string Title { get; set; }
        public string Src { get; set; }
    }
}
