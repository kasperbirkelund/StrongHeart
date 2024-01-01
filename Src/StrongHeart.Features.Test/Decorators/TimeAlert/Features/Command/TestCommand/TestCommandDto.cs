using System.Diagnostics;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.TimeAlert.Features.Command.TestCommand;

[DebuggerStepThrough]
public record TestCommandDto(int TimeToExecuteOnSeconds) : IRequestDto;