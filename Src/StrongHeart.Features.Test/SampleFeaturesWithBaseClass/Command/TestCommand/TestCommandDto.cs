using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeaturesWithBaseClass.Command.TestCommand;

public record TestCommandDto(bool ShouldSucceed) : IRequestDto;