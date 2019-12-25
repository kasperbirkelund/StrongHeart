using FluentValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand
{
    public class TestCommandRequestValidator : AbstractValidator<TestCommandRequest>
    {
        public TestCommandRequestValidator()
        {
            RuleFor(x => x.Model.Age).GreaterThan(0);
        }
    }
}