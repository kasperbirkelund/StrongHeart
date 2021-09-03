using System.Threading.Tasks;

namespace StrongHeart.Features.Core.Events
{
    public interface IEventHandler<TEvent, TMetadata>
        where TEvent : class, IEvent
        where TMetadata : class, IMetadata
    {
        Task Execute(EventMessage<TEvent, TMetadata> @event);
    }
}