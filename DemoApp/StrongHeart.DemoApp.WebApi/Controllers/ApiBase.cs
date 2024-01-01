using StrongHeart.Core.Security;
using StrongHeart.Features.AspNetCore;

namespace StrongHeart.DemoApp.WebApi.Controllers;

public abstract class ApiBase : StrongHeartApiBase//, IDocumentationDescriber
{
    private readonly ICallerProvider _callerProvider;

    protected ApiBase(ICallerProvider callerProvider)
    {
        _callerProvider = callerProvider;
    }

    protected ICaller GetCaller()
    {
        return _callerProvider.GetCurrentCaller();
    }

    //public virtual string? DocName => DocumentationConstants.Setup;
    //public virtual int? Order => 2;

    //IEnumerable<ISection> IDocumentationDescriber.GetDocumentationSections(DocumentationGenerationContext context)
    //{
    //    yield return new TextSection($"{nameof(StrongHeartApiBase)} is only applicable on WebApis. If not a WebApi remove {nameof(StrongHeartApiBase)}");
    //    yield return new CodeCommentSection(GetType());
    //}
}