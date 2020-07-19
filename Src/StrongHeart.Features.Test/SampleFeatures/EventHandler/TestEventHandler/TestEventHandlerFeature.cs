using System;
using System.Collections.Generic;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Audit;
using StrongHeart.Features.Test.SampleFeatures.Command.TestCommand;

namespace StrongHeart.Features.Test.SampleFeatures.EventHandler.TestEventHandler
{
    public class TestEventHandlerFeature : EventHandlerFeatureBase<TestEvent, TestCommandRequest, TestCommandDto>, IAuditable<EventMessage<TestEvent>>
    {
        protected override TestCommandRequest GetRequest(EventMessage<TestEvent> @event)
        {
            throw new NotImplementedException();
        }

        public override Func<EventMessage<TestEvent>, bool> IsOnBehalfOfOtherSelector => request => false;
        public override AuditOptions AuditOptions => new AuditOptions(Guid.Parse("917d7fba-1f85-46db-825e-d609b3786335"), "TestEventHandler", false);
        public override Func<EventMessage<TestEvent>, IEnumerable<Guid?>> CorrelationKeySelector => request => new List<Guid?>();

        public TestEventHandlerFeature(ICommandFeature<TestCommandRequest, TestCommandDto> command) : base(command)
        {
        }
    }
}