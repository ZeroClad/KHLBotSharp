namespace KHLBotSharp.Core.Models
{
    public interface ICardComponent
    {
        string Type { get; }
    }

    public interface ICardBodyComponent : ICardComponent { }
    public interface ICardContent : ICardComponent { }
    public interface ICardTextGroup : ICardComponent { }
    public interface ICardParagraphComponent : ICardTextGroup { }
    public interface ICardTextComponent : ICardTextGroup, ICardContent { }
    public interface ICardImageComponent : ICardContent { }
    public interface ICardButtonComponent : ICardComponent { }
}
