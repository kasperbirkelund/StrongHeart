﻿using StrongHeart.Features.Core.Events;

namespace StrongHeart.Features.Decorators
{
    public interface IEventHandlerDecorator<TEvent, TMetadata>
        where TEvent : class, IEvent
        where TMetadata : class, IMetadata
    {
        IEventHandler<TEvent, TMetadata> GetInnerFeature();
    }
}