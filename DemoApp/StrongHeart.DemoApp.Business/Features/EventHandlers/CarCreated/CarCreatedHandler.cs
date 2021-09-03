using System.Threading.Tasks;
using StrongHeart.DemoApp.Business.Events;
using StrongHeart.DemoApp.Business.Features.Commands.NewCarCustomerNotification;
using StrongHeart.Features.Core;
using StrongHeart.Features.Core.Events;

namespace StrongHeart.DemoApp.Business.Features.EventHandlers.CarCreated
{
    public class CarCreatedHandler : EventHandlerFeatureBase<CarCreatedEvent>
    {
        private readonly ICommandFeature<NewCarCustomerNotificationRequest, NewCarCustomerNotificationDto> _feature;

        public CarCreatedHandler(ICommandFeature<NewCarCustomerNotificationRequest, NewCarCustomerNotificationDto> feature)
        {
            _feature = feature;
        }

        public override async Task Execute(EventMessage<CarCreatedEvent, DemoAppSpecificMetadata> @event)
        {
            await _feature.Execute(new NewCarCustomerNotificationRequest(new NewCarCustomerNotificationDto(), null));
        }
    }
}
