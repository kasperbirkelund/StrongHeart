using System.Collections.Generic;
using StrongHeart.Core.Security;
using StrongHeart.DemoApp.WebApi.Services;
using StrongHeart.Features.AspNetCore;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    //DOC-START
    public abstract class ApiBase : StrongHeartApiBase, IDocumentationDescriber
    {
        private readonly IClaimsProvider _claimsProvider;

        protected ApiBase(IClaimsProvider claimsProvider)
        {
            _claimsProvider = claimsProvider;
        }

        protected ICaller GetCaller()
        {
            //Read certificate, token, http context, whatever and extract claims
            return new WebApiCaller(_claimsProvider.ExtractClaims());
        }
        //DOC-END

        public virtual string? DocName => DocumentationConstants.Setup;
        public virtual int? Order => 2;

        IEnumerable<ISection> IDocumentationDescriber.GetDocumentationSections(DocumentationGenerationContext context)
        {
            yield return new TextSection($"{nameof(StrongHeartApiBase)} is only applicable on WebApis. If not a WebApi remove {nameof(StrongHeartApiBase)}");
            yield return new CodeCommentSection(GetType());
        }
    }
}