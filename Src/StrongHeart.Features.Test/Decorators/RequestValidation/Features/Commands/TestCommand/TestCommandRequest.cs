using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand;

public record TestCommandRequest(ICaller Caller, TestCommandDto Model) : IRequest<TestCommandDto>;