using System;
using System.Diagnostics;

namespace StrongHeart.Core.EventAggregator
{
    [DebuggerStepThrough]
    [DebuggerDisplay("Event:{EventType.Name} Handler{HandlerType.Name}")]
    public class EventSubscription
    {
        /// <summary>
        /// Class to contain the data of an event subscription
        /// </summary>
        /// <param name="eventType">The type of the event</param>
        /// <param name="handlerType">The type of the event handler</param>
        /// <param name="handlerFunc">A delegate to initialize the handler</param>
        public EventSubscription(Type eventType, Type handlerType, Func<object> handlerFunc)
        {
            EventType = eventType;
            HandlerType = handlerType;
            HandlerFunc = handlerFunc;
        }

        public Type EventType { get; }
        public Type HandlerType { get; }
        public Func<object> HandlerFunc { get; }
    }
}