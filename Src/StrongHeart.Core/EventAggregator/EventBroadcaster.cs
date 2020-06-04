//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using StrongHeart.Core.Events;

//namespace StrongHeart.Core.EventAggregator
//{
//    public class EventBroadcaster : IEventBroadcaster
//    {
//        private readonly ApplicationLogLevel _minLogLevel;
//        private readonly List<EventSubscription> _subscriptions = new List<EventSubscription>();

//        internal static EventBroadcaster Create(bool doLogToConsole/*, ConnectionString connectionString*/)
//        {
//            var level = ApplicationLogLevel.Information;
//            var eb =  new EventBroadcaster(level);
//            Dictionary<string, string> properties = new Dictionary<string, string>
//            {
//                {"Version", Assembly.GetEntryAssembly()?.GetName().Version.ToString()}
//            };

//            //ILogger logger = GetSeriLog(doLogToConsole, connectionString.Value, properties, level);
//            //eb.AddSubscriptions(new SerilogEventHandler(logger));
//            return eb;
//        }

//        private EventBroadcaster(ApplicationLogLevel minLogLevel)
//        {
//            _minLogLevel = minLogLevel;
//        }
        
//        public void Subscribe<TEvent>(Expression<Func<IEventHandler<TEvent>>> getHandler)
//            where TEvent : IDomainEvent
//        {
//            //let's find out which concrete handler type we have. Expression is the only way we can get this information
//            Type handlerType = getHandler.Body.Type;
//            if (_subscriptions.Any(x => x.EventType == typeof(TEvent) && x.HandlerType == handlerType))
//            {
//                // oops registering the same handler<->event combination more than one tine is no-good
//                throw new InvalidOperationException($"Handler '{handlerType.Name}' has already been registered with event '{typeof (TEvent).Name}'");
//            }

//            //validation went well. Now we can compile the expression and add the subscription.
//            _subscriptions.Add(new EventSubscription(typeof(TEvent), handlerType, getHandler.Compile()));
//        }

//        public void PublishEvent<TEvent>(TEvent @event) where TEvent : IDomainEvent
//        {
//            LogMessageEvent evt = @event as LogMessageEvent;
//            if (evt != null && evt.Level < _minLogLevel)
//            {
//                //this will boost performance - just a bit
//                return;
//            }

//            if (@event.GetType() != typeof(TEvent))
//            {
//                throw new ArgumentException("Cannot use abstract type/interface type when publishing an event.");
//            }

//            //Find the list of event-handlers
//            Func<IEventHandler<TEvent>>[] handlers = _subscriptions
//                .Where(x => typeof(TEvent) == x.EventType)
//                .Select(x => (Func<IEventHandler<TEvent>>)x.HandlerFunc) //odd but required typecast
//                .ToArray();

//            //loop through all handlers to invoke them one by one. Order shouldn't matter...? :)
//            foreach (Func<IEventHandler<TEvent>> handler in handlers)
//            {
//                IEventHandler<TEvent> handlerInstance = handler(); //pick up the actual handler from the delegate
//                handlerInstance.Handle(@event); //invoke the eventhandler. "Step into" here to debug
//            }
//        }
//    }
//}