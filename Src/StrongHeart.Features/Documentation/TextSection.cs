namespace StrongHeart.Features.Documentation
{
    public class TextSection : ISection
    {
        public TextSection(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public void Accept(ISectionVisitor visitor)
        {
            visitor.VisitText(this);
        }
    }
}