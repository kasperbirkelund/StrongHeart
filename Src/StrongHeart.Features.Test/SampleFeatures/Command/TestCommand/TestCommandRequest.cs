using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeatures.Command.TestCommand;

public record TestCommandRequest(ICaller Caller, TestCommandDto Model) : IRequest<TestCommandDto>;