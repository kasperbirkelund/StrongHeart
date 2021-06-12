using System;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand
{
    public class TestCommandDto : IRequestDto
    {
        public TestCommandDto(int age, DateTime birthDay)
        {
            Age = age;
            BirthDay = birthDay;
        }

        public int Age { get; }
        public DateTime BirthDay { get; }
    }
}
