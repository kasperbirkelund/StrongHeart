using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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

        public async Task Publish<T>(T @event) where T : class, IEvent
        {
            DemoAppSpecificMetadata metadata = new();

            using (var scope = _serviceProvider.CreateScope())
            {
                var feature = scope.ServiceProvider.GetRequiredService<IEventHandlerFeature<T, DemoAppSpecificMetadata>>();
                await feature.Execute(new EventMessage<T, DemoAppSpecificMetadata>(metadata, @event));
            }
        }

        //switch (@event)
        //{
        //    case CarCreatedEvent e:
        //        {
        //            using var scope = _serviceProvider.CreateScope();
        //            var feature = scope.ServiceProvider.GetRequiredService<IEventHandlerFeature<CarCreatedEvent, DemoAppSpecificMetadata>>();
        //            await feature.Execute(new EventMessage<CarCreatedEvent, DemoAppSpecificMetadata>(metadata, e));
        //            break;
        //        }
        //}
    }
}