//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using StrongHeart.Features.Decorators.Audit;

//namespace StrongHeart.Features.Core
//{
//    public abstract class EventHandlerFeatureBase<TEvent, TRequest, TRequestDto> 
//        : IEventHandlerFeature<TEvent>,
//            IAuditable<EventMessage<TEvent>> where TEvent : class, IEvent
//        where TRequest : IRequest<TRequestDto>
//        where TRequestDto : IRequestDto
//    {
//        protected EventHandlerFeatureBase(ICommandFeature<TRequest, TRequestDto> command)
//        {
//            Command = command;
//        }

//        private ICommandFeature<TRequest, TRequestDto> Command { get; }

//        public Task<Result> Handle(EventMessage<TEvent> @event)
//        {
//            TRequest request = GetRequest(@event);
//            return Command.Execute(request);
//        }

//        protected abstract TRequest GetRequest(EventMessage<TEvent> @event);
//        public abstract Func<EventMessage<TEvent>, bool> IsOnBehalfOfOtherSelector { get; }
//        public abstract AuditOptions AuditOptions { get; }
//        public abstract Func<EventMessage<TEvent>, IEnumerable<Guid?>> CorrelationKeySelector { get; }
//    }
//}