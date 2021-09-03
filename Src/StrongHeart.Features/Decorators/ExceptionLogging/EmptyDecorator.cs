//using System.Threading.Tasks;
//using StrongHeart.Features.Core.Events;

//namespace StrongHeart.Features.Decorators.ExceptionLogging
//{
//    public sealed class EmptyDecorator<TEvent, TMetadata> : IEventHandler<TEvent, TMetadata>, IEventHandlerDecorator<TEvent, TMetadata> 
//        where TMetadata : class, IMetadata
//        where TEvent : class, IEvent
//    {
//        private readonly IEventHandler<TEvent, TMetadata> _inner;

//        public EmptyDecorator(IEventHandler<TEvent, TMetadata> inner)
//        {
//            _inner = inner;
//        }

//        public Task Execute(EventMessage<TEvent, TMetadata> @event)
//        {
//            return GetInnerFeature().Execute(@event);
//        }

//        public IEventHandler<TEvent, TMetadata> GetInnerFeature() => _inner;
//    }
//}
