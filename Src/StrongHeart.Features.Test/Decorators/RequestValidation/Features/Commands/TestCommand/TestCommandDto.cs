using System;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand
{
    public record TestCommandDto(int Age, DateTime BirthDay) : IRequestDto;
}
