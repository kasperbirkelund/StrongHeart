using System.Diagnostics;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.Retry.Features.Command.TestCommand;

[DebuggerStepThrough]
public record TestCommandDto(int Age) : IRequestDto;