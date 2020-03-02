using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Audit;

namespace StrongHeart.Features.Test.SampleFeatures.EventHandler.TestEventHandler
{
    public class TestEventHandlerFeature : IEventHandlerFeature<TestEvent>, IAuditable<EventMessage<TestEvent>>
    {
        public Task<Result> Handle(EventMessage<TestEvent> @event)
        {
            return Task.FromResult(Result.Success());

        }

        public Func<EventMessage<TestEvent>, bool> IsOnBehalfOfOtherSelector => request => false;
        public AuditOptions AuditOptions => new AuditOptions(Guid.Parse("917d7fba-1f85-46db-825e-d609b3786335"), "TestEventHandler", false);
        public Func<EventMessage<TestEvent>, IEnumerable<Guid?>> CorrelationKeySelector => request => new List<Guid?>();
    }
}