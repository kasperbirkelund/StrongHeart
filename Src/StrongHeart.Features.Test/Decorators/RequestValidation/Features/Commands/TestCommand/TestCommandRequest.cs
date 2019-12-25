using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand
{
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