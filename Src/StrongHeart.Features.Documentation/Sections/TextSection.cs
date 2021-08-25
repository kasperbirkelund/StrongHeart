using StrongHeart.Features.Documentation.Visitors;

namespace StrongHeart.Features.Documentation.Sections
{
    public class TextSection : ISection
    {
        public TextSection(string text, bool isHeader = false)
        {
            Text = text;
            IsHeader = isHeader;
        }

        public string Text { get; }
        public bool IsHeader { get; }

        public void Accept(ISectionVisitor visitor)
        {
            visitor.VisitText(this);
        }
    }
}