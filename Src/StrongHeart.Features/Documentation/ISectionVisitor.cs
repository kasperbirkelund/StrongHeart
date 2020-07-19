namespace StrongHeart.Features.Documentation
{
    public interface ISectionVisitor
    {
        void VisitText(TextSection section);
        void VisitTable<T>(TableSection<T> section);
    }
}