using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KHLBotSharp.Core.Models
{
    public class SectionModule : ICardBodyComponent
    {
        public string Type => "section";
        public ICardComponent Text { get; set; }
        [JsonIgnore]
        public Direction? Mode
        {
            get
            {
                if (Enum.TryParse(ModeString,true, out Direction theme))
                {
                    return theme;
                }
                return null;
            }
            set
            {
                ModeString = Enum.GetName(typeof(Direction), value).ToLower();
            }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
        public string ModeString { get; set; }

        [JsonIgnore]
        public List<ICardTextGroup> Accessory { get; set; }

        [JsonIgnore]
        public ICardTextGroup AccessoryObject { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonProperty("accessory", NullValueHandling = NullValueHandling.Ignore)]
        public object AccessoryReturn
        {
            get
            {
                if(Accessory != null)
                {
                    return Accessory;
                }
                else
                {
                    return AccessoryObject;
                }
            }
        }

        public SectionModule AddAccessory(params ICardTextGroup[] cardComponents)
        {
            if(Accessory == null)
            {
                Accessory = new List<ICardTextGroup>();
            }
            Accessory.AddRange(cardComponents);
            return this;
        }
        public SectionModule AddAccessory(ICardTextGroup cardComponents)
        {
            AccessoryObject = cardComponents;
            return this;
        }
    }

    public enum Direction
    {
        Left,
        Right
    }
}
