using System.Collections.Generic;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.Features.Documentation
{
    public interface IDocumentationDescriber
    {
        IEnumerable<ISection> GetDocumentationSections(DocumentationGenerationContext context);
    }
}
