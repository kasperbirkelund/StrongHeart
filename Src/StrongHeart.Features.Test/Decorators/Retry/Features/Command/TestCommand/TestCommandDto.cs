using System.Diagnostics;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.Retry.Features.Command.TestCommand
{
    [DebuggerStepThrough]
    public class TestCommandDto : IRequestDto
    {
        public TestCommandDto(int age)
        {
            Age = age;
        }

        public int Age { get; }
    }
}
