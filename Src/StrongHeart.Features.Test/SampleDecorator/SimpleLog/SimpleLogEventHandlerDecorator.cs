using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;

namespace StrongHeart.Features.Test.SampleDecorator.SimpleLog
{
    //[DebuggerStepThrough]
    public sealed class SimpleLogEventHandlerDecorator<TEvent> : SimpleLogDecoratorBase, IEventHandlerFeature<TEvent>, IEventHandlerDecorator<TEvent> 
        where TEvent : class, IEvent
    {
        private readonly IEventHandlerFeature<TEvent> _inner;

        public SimpleLogEventHandlerDecorator(IEventHandlerFeature<TEvent> inner, ISimpleLog simpleLog) 
            : base(simpleLog)
        {
            _inner = inner;
        }

        public Task<Result> Handle(EventMessage<TEvent> @event)
        {
            return Invoke(_inner.Handle, @event);
        }

        public IEventHandlerFeature<TEvent> GetInnerFeature()
        {
            return _inner;
        }
    }
}