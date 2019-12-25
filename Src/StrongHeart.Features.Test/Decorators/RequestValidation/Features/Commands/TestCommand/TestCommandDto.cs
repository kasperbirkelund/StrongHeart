using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand
{
    public class TestCommandDto : IRequestDto
    {
        public TestCommandDto(int age)
        {
            Age = age;
        }

        public int Age { get; }
    }
}
