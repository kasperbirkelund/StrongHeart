using System.Collections.Generic;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.Features.Documentation
{
    public interface IDocumentationDescriber
    {
        string? DocName { get; }
        int? Order { get; }
        IEnumerable<ISection> GetDocumentationSections(DocumentationGenerationContext context);
    }
}
