using System.Diagnostics;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.TimeAlert.Features.Command.TestCommand
{
    [DebuggerStepThrough]
    public record TestCommandRequest(ICaller Caller, TestCommandDto Model) : IRequest<TestCommandDto>;
}