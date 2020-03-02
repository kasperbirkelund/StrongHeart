using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators
{
    public interface IEventHandlerDecorator<TEvent> where TEvent : class, IEvent
    {
        IEventHandlerFeature<TEvent> GetInnerFeature();
    }
}