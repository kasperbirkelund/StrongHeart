using System.Threading.Tasks;

namespace StrongHeart.Features.Core
{
    public interface IEventHandlerFeature<TEvent> where TEvent : class, IEvent
    {
        Task<Result> Handle(EventMessage<TEvent> @event);
    }
}