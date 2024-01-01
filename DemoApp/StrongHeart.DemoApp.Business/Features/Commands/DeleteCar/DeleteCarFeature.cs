using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Commands.DeleteCar;

public partial class DeleteCarFeature
{
    public override Task<Result> Execute(DeleteCarRequest request)
    {
        //Do work
        return Task.FromResult(Result.Success());
    }

    protected override IEnumerable<ValidationMessage> ValidateRequest(DeleteCarRequest request)
    {
        yield break;
    }

    protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
    {
        yield break;
    }
}