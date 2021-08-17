using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.DemoApp.Business.Events;
using StrongHeart.DemoApp.Business.Features.EventHandlers;
using StrongHeart.Features.Core.Events;

namespace StrongHeart.DemoApp.WebApi
{
    public class SimpleEventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public SimpleEventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish<T>(T @event) where T : IEvent
        {
            DemoAppSpecificMetadata metadata = new();
            switch (@event)
            {
                case CarCreatedEvent e:
                    {
                        var feature = _serviceProvider.GetRequiredService<IEventHandlerFeature<CarCreatedEvent, DemoAppSpecificMetadata>>();
                        await feature.Execute(new EventMessage<CarCreatedEvent, DemoAppSpecificMetadata>(metadata, e));
                        break;
                    }
            }
        }
    }
}
