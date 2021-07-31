using System.Collections.Generic;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features
{
    public abstract class FeatureBase : IDocumentationDescriber
    {
        public virtual IEnumerable<ISection> GetDocumentationSections(DocumentationGenerationContext context)
        {
            yield return new HeaderSection(GetType().Name);
            foreach (ISection section in OnGetDocumentationSections(context))
            {
                yield return section;
            }
        }

        protected abstract IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context);
    }
}