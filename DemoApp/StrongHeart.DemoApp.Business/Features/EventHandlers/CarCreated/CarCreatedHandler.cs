using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.DemoApp.Business.Events;
using StrongHeart.DemoApp.Business.Features.Commands.NewCarCustomerNotification;
using StrongHeart.Features.Core;
using StrongHeart.Features.Core.Events;

namespace StrongHeart.DemoApp.Business.Features.EventHandlers.CarCreated;

public class CarCreatedHandler : EventHandlerBase<CarCreatedEvent>
{
    private readonly ICommandFeature<NewCarCustomerNotificationRequest, NewCarCustomerNotificationDto> _feature;
    private readonly ICallerProvider _callerProvider;

    public CarCreatedHandler(ICommandFeature<NewCarCustomerNotificationRequest, NewCarCustomerNotificationDto> feature, ICallerProvider callerProvider)
    {
        _feature = feature;
        _callerProvider = callerProvider;
    }

    public override async Task Execute(EventMessage<CarCreatedEvent, DemoAppSpecificMetadata> evt)
    {
        await _feature.Execute(new NewCarCustomerNotificationRequest(new NewCarCustomerNotificationDto(), _callerProvider.GetCurrentCaller()));
    }
}