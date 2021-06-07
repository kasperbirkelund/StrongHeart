using System.Collections.Generic;
using StrongHeart.Core.Security;

namespace StrongHeart.Features.Documentation
{
    public interface IDocumentationDescriber
    {
        IEnumerable<ISection> GetDocumentationSections(ICaller caller);
    }
}
