using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace KHLBotSharp.Core.Models
{
    public class CardBuilder : List<Card>
    {
        public Card Create()
        {
            var card = new Card();
            Add(card);
            return card;
        }

        public CardBuilder Create(params ICardBodyComponent[] components)
        {
            var card = new Card();
            card.AddModules(components);
            Add(card);
            return this;
        }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var json = JsonConvert.SerializeObject(this, Formatting.None, settings);
            return json;
        }
    }
}
