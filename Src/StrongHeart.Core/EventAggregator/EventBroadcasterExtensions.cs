using StrongHeart.Core.Events;

namespace StrongHeart.Core.EventAggregator
{
    public static class EventBroadcasterExtensions
    {
        public static void AddSubscriptions<THandler>(this IEventBroadcaster broadcaster, THandler handlerInstance) where THandler : class,
                        IEventHandler<LogMessageEvent>,
                        IEventHandler<ExceptionThrownEvent>
        {
            broadcaster.Subscribe<ExceptionThrownEvent>(() => handlerInstance);
            broadcaster.Subscribe<LogMessageEvent>(() => handlerInstance);
        }
    }
}