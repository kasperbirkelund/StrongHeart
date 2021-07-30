using StrongHeart.Features.Documentation.Visitors;

namespace StrongHeart.Features.Documentation.Sections
{
    public class HeaderSection : ISection
    {
        public HeaderSection(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public void Accept(ISectionVisitor visitor)
        {
            visitor.VisitHeader(this);
        }
    }
}