using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.DemoApp.Business.Events;
using StrongHeart.Features.Core;
using StrongHeart.Features.Core.Events;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Commands.CreateCar;

public class CreateCarFeature : CommandFeatureBase<CreateCarRequest, CreateCarDto>
{
    private readonly IEventPublisher _eventPublisher;

    public CreateCarFeature(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }
    public override async Task<Result> Execute(CreateCarRequest request)
    {
        //DOC-START Special customer rule...
        bool isCustomer = IsCustomer();
        if (isCustomer)
        {
            SendGift();
        }
        //DOC-END
        await _eventPublisher.Publish(new CarCreatedEvent(request.Id));
        return Result.Success();
    }

    private bool IsCustomer()
    {
        return true;
    }

    private void SendGift()
    {
        //Do something
    }

    protected override IEnumerable<ValidationMessage> ValidateRequest(CreateCarRequest request)
    {
        //Just any insane validation

        //DOC-START Validation rule
        if (request.Model.Brand != "Skoda")
        {
            yield return "Model must be Skoda";
        }
        //DOC-END
    }

    protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
    {
        yield return new CodeCommentSection(GetType());
    }
}

public class CreateCarDto : IRequestDto
{
    public CreateCarDto(string brand)
    {
        Brand = brand;
    }

    public string Brand { get; }
}

public class CreateCarRequest : IRequest<CreateCarDto>
{
    public CreateCarRequest(Guid id, CreateCarDto model, ICaller caller)
    {
        Id = id;
        Caller = caller;
        Model = model;
    }

    public Guid Id { get; }
    public ICaller Caller { get; }
    public CreateCarDto Model { get; }
}