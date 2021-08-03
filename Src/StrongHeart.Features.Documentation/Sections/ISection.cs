using StrongHeart.Features.Documentation.Visitors;

namespace StrongHeart.Features.Documentation.Sections
{
    public interface ISection
    {
        void Accept(ISectionVisitor visitor);
    }
}