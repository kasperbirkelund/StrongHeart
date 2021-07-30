using System.Collections.Generic;
using StrongHeart.Core.Security;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features
{
    public abstract class FeatureBase : IDocumentationDescriber
    {
        public virtual IEnumerable<ISection> GetDocumentationSections(ICaller caller)
        {
            yield return new HeaderSection(GetType().Name);
            foreach (ISection section in GetLocalDocumentationSections(caller))
            {
                yield return section;
            }
        }

        protected abstract IEnumerable<ISection> GetLocalDocumentationSections(ICaller caller);
    }
}