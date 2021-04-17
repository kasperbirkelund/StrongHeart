namespace StrongHeart.Features.Documentation
{
    public interface ISection
    {
        void Accept(ISectionVisitor visitor);
    }
}