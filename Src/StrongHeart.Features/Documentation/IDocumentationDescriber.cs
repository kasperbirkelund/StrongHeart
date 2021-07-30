using System.Collections.Generic;
using StrongHeart.Core.Security;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.Features.Documentation
{
    public interface IDocumentationDescriber
    {
        IEnumerable<ISection> GetDocumentationSections(ICaller caller);
    }
}
