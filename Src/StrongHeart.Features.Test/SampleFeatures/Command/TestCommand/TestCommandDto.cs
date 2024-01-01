using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeatures.Command.TestCommand;

public record TestCommandDto(bool ShouldSucceed) : IRequestDto;