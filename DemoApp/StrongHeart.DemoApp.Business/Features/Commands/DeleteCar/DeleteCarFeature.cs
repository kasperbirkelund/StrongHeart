using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Commands.DeleteCar;

public record DeleteCarCompletedAction(int NumberOfCarsDeleted);

public class DeleteCarFeature : CommandFeatureBase<DeleteCarRequest, DeleteCarDto>
{
    public override Task<Result> Execute(DeleteCarRequest request)
    {
        int numberOfCarsDeleted = 1; //Just a dummy value
        request.CompletedAction(new DeleteCarCompletedAction(numberOfCarsDeleted));
        return Task.FromResult(Result.Success());
    }

    public override IEnumerable<ValidationMessage> ValidateRequest(DeleteCarRequest request)
    {
        yield break;
    }

    protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
    {
        yield break;
    }
}

public class DeleteCarRequest : IRequest<DeleteCarDto>, ICompletedAction<DeleteCarCompletedAction>
{
    public DeleteCarRequest(DeleteCarDto model, ICaller caller, Action<DeleteCarCompletedAction> completedAction)
    {
        Caller = caller;
        CompletedAction = completedAction;
        Model = model;
    }

    public ICaller Caller { get; }
    public DeleteCarDto Model { get; }
    public Action<DeleteCarCompletedAction> CompletedAction { get; }
}

public class DeleteCarDto : IRequestDto
{
    public Guid Id { get; }

    public DeleteCarDto(Guid id)
    {
        Id = id;
    }
}