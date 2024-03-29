using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Commands.DeleteCar;

public class DeleteCarFeature : CommandFeatureBase<DeleteCarRequest, DeleteCarDto>
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

public class DeleteCarRequest : IRequest<DeleteCarDto>
{
    public DeleteCarRequest(DeleteCarDto model, ICaller caller)
    {
        Caller = caller;
        Model = model;
    }

    public ICaller Caller { get; }
    public DeleteCarDto Model { get; }
}

public class DeleteCarDto : IRequestDto
{
    public Guid Id { get; }

    public DeleteCarDto(Guid id)
    {
        Id = id;
    }
}