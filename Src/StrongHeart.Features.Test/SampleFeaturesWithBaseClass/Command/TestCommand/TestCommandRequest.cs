using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeaturesWithBaseClass.Command.TestCommand;

public record TestCommandRequest(ICaller Caller, TestCommandDto Model) : IRequest<TestCommandDto>;