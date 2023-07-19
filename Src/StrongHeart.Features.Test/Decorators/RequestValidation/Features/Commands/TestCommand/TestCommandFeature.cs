using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand
{
    public class TestCommandFeature : ICommandFeature<TestCommandRequest, TestCommandDto>, IRequestValidator<TestCommandRequest>
    {
        private readonly TestCommandRequestValidator _validator;

        public TestCommandFeature(TestCommandRequestValidator validator)
        {
            _validator = validator;
        }
        public Task<Result> Execute(TestCommandRequest request)
        {
            return Task.FromResult(Result.Success());
        }

        public IEnumerable<ValidationMessage> Validate(TestCommandRequest request)
        {
            return FluentValidationMapper.Map(_validator.Validate(request));
        }
    }
}