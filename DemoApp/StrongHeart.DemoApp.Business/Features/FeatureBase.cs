using System.Collections.Generic;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features;

public abstract class FeatureBase : IDocumentationDescriber
{
    public string? DocName => null;
    public int? Order => null;

    public virtual IEnumerable<ISection> GetDocumentationSections(DocumentationGenerationContext context)
    {
        yield return new TextSection(GetType().Name, true);
        foreach (ISection section in OnGetDocumentationSections(context))
        {
            yield return section;
        }
    }

    protected abstract IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context);
}