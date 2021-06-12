using FluentValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand
{
    public class TestCommandRequestValidator : AbstractValidator<TestCommandRequest>
    {
        public TestCommandRequestValidator(IValidationDependency validationDependency)
        {
            RuleFor(x => x.Model.Age).GreaterThan(0);
            RuleFor(x => x.Model.BirthDay).CustomAsync(validationDependency.IsValidDate);
        }
    }
}