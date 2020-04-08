using System;
using System.Linq.Expressions;

namespace StrongHeart.Core.EventAggregator
{
    public interface IEventBroadcaster : IEventPublisher
    {
        void Subscribe<TEvent>(Expression<Func<IEventHandler<TEvent>>> getHandler) where TEvent : IDomainEvent;
    }
}
