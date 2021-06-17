using System.Diagnostics;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.Retry.Features.Command.TestCommand
{
    [DebuggerStepThrough]
    public class TestCommandRequest : IRequest<TestCommandDto>
    {
        public TestCommandRequest(ICaller caller, TestCommandDto model)
        {
            Caller = caller;
            Model = model;
        }

        public ICaller Caller { get; }
        public TestCommandDto Model { get; }
    }
}