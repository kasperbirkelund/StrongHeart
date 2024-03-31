using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Documentation;
using StrongHeart.Features.Documentation.Sections;

namespace StrongHeart.DemoApp.Business.Features.Commands.NewCarCustomerNotification;

public class NewCarCustomerNotificationFeature : CommandFeatureBase<NewCarCustomerNotificationRequest, NewCarCustomerNotificationDto>
{
    protected override IEnumerable<ISection> OnGetDocumentationSections(DocumentationGenerationContext context)
    {
        yield break;
    }

    public override Task<Result> Execute(NewCarCustomerNotificationRequest request)
    {
        //do stuff
        return Task.FromResult(Result.QueuedForLaterExecution());
    }

    public override IEnumerable<ValidationMessage> ValidateRequest(NewCarCustomerNotificationRequest request)
    {
        yield break;
    }
}

public class NewCarCustomerNotificationRequest : IRequest<NewCarCustomerNotificationDto>
{
    public NewCarCustomerNotificationRequest(ICaller caller, NewCarCustomerNotificationDto model)
    {
        Caller = caller;
        Model = model;
    }

    public ICaller Caller { get; }
    public NewCarCustomerNotificationDto Model { get; }
}

public class NewCarCustomerNotificationDto : IRequestDto
{
}