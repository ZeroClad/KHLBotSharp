using System;
using System.Collections.Generic;
using System.Text;

namespace KHLBotSharp.Core.Models
{
    public class CountdownModule : ICardBodyComponent
    {
        public string Type => "countdown";
        public long EndTime { get; set; }
        public long StartTime { get; set; }
    }
}
