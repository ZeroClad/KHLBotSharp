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
            if (Elements == null)
            {
                Elements = new List<ICardButtonComponent>();
            }
            Elements.AddRange(buttonComponents);
            return this;
        }

        public ButtonModule AddButton(string text, string value, CardTheme theme = CardTheme.Secondary)
        {
            var button = new ButtonModule { Text = new TextModule { Content = text }, Value = value, Theme = theme };
            Elements.Add(button);
            return button;
        }
    }
}
