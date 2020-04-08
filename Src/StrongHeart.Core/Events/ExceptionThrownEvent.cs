using System;
using StrongHeart.Core.EventAggregator;

namespace StrongHeart.Core.Events
{
    public class ExceptionThrownEvent : IDomainEvent
    {
        public Exception Exception { get; }

        public ExceptionThrownEvent(Exception exception)
        {
            Exception = exception;
        }
    }
}