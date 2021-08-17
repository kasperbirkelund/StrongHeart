using System.Threading.Tasks;
using StrongHeart.Features.Core.Events;

namespace StrongHeart.DemoApp.Business.Features.EventHandlers
{
    public abstract class EventHandlerFeatureBase<TEvent> : IEventHandlerFeature<TEvent, DemoAppSpecificMetadata> 
        where TEvent : class, IEvent
    {
        public abstract Task Execute(EventMessage<TEvent, DemoAppSpecificMetadata> @event);
    }
}