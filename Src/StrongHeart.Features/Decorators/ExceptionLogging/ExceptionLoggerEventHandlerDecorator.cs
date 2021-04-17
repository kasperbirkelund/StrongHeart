//using System.Threading.Tasks;
//using StrongHeart.Features.Core;

//namespace StrongHeart.Features.Decorators.ExceptionLogging
//{
//    //[DebuggerStepThrough]
//    public sealed class ExceptionLoggerEventHandlerDecorator<TEvent> : ExceptionLoggerDecoratorBase, IEventHandlerFeature<TEvent>, IEventHandlerDecorator<TEvent> 
//        where TEvent : class, IEvent
//    {
//        private readonly IEventHandlerFeature<TEvent> _inner;

//        public ExceptionLoggerEventHandlerDecorator(IEventHandlerFeature<TEvent> inner, IExceptionLogger logger) 
//            : base(logger)
//        {
//            _inner = inner;
//        }

//        public Task<Result> Handle(EventMessage<TEvent> @event)
//        {
//            return Invoke(_inner.Handle, @event);
//        }

//        public IEventHandlerFeature<TEvent> GetInnerFeature()
//        {
//            return _inner;
//        }
//    }
//}