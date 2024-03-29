using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Commands.UpdateCar;

public class UpdateCarFeature : CommandFeatureBase<UpdateCarRequest, UpdateCarDto>
{
    private readonly string[] _validModelNames = { "Skoda" };

    public override Task<Result> Execute(UpdateCarRequest request)
    {
        //Do work

        //Put the request on a queue and return "QueuedForLaterExecution" -
        //OR do the job immediately and return Result.Success()
        return Task.FromResult(Result.QueuedForLaterExecution());
    }

    protected override IEnumerable<ValidationMessage> ValidateRequest(UpdateCarRequest request)
    {
        if (!_validModelNames.Contains(request.Model.Brand))
        {
            yield return "Model must be " + string.Join(", ", _validModelNames);
        }
    }

    protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
    {
        yield return new TextSection("Valid request values: " + string.Join(", ", _validModelNames));
    }
}

public class UpdateCarDto : IRequestDto
{
    public UpdateCarDto(Guid id, string? brand)
    {
        Id = id;
        Brand = brand;
    }

    public Guid Id { get; }
    public string? Brand { get; }
}

public class UpdateCarRequest : IRequest<UpdateCarDto>
{
    public UpdateCarRequest(UpdateCarDto model, ICaller caller)
    {
        Caller = caller;
        Model = model;
    }

    public ICaller Caller { get; }
    public UpdateCarDto Model { get; }
}