//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using StrongHeart.Features.Core;

//namespace StrongHeart.Features.Decorators.Audit
//{
//    //[DebuggerStepThrough]
//    public sealed class AuditLoggingEventHandlerDecorator<TEvent> : AuditLoggingDecoratorBase, IEventHandlerFeature<TEvent>, IEventHandlerDecorator<TEvent> 
//        where TEvent : class, IEvent
//    {
//        private readonly IEventHandlerFeature<TEvent> _inner;

//        public AuditLoggingEventHandlerDecorator(IEventHandlerFeature<TEvent> inner, IFeatureAuditRepository repository) 
//            : base(repository)
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

//        protected override Guid GetUniqueFeatureId()
//        {
//            return GetInterface().AuditOptions.UniqueFeatureId;
//        }

//        public override AuditOptions AuditOptions => GetInterface().AuditOptions;
//        public override IEnumerable<Guid?> GetCorrelationKey<T>(T request)
//        {
//            Func<EventMessage<TEvent>, IEnumerable<Guid?>> selector = GetInterface().CorrelationKeySelector;
//            return selector(request as EventMessage<TEvent>);
//        }

//        public override bool GetIsOnBehalfOfOther<TRequest1>(TRequest1 request)
//        {
//            Func<EventMessage<TEvent>, bool> selector = GetInterface().IsOnBehalfOfOtherSelector;
//            return selector(request as EventMessage<TEvent>);
//        }

//        private IAuditable<EventMessage<TEvent>> GetInterface()
//        {
//            return (IAuditable<EventMessage<TEvent>>)this.GetInnerMostFeature();
//        }
//    }
//}