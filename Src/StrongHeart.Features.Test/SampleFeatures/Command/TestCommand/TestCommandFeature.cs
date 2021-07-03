using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Authorization;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Decorators.Retry;

namespace StrongHeart.Features.Test.SampleFeatures.Command.TestCommand
{
    public class TestCommandFeature : ICommandFeature<TestCommandRequest, TestCommandDto>, IAuthorizable, IRequestValidatable, IRetryable
    {
        public Task<Result> Execute(TestCommandRequest request)
        {
            Result result = request.Model.ShouldSucceed ? Result.Success() : Result.Failure("Forced to fail");
            return Task.FromResult(result);
        }

        public IEnumerable<Claim> GetRequiredClaims()
        {
            yield break;
        }

        public IValidator GetValidator()
        {
            return new SampleValidator();
        }

        public bool WhenExceptionIsThrownShouldIRetry(Exception exception, int currentAttempt)
        {
            return false;
        }

        private class SampleValidator : IValidator
        {
            public ValidationResult Validate(IValidationContext context)
            {
                throw new NotSupportedException();
            }

            public Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellation = new())
            {
                return Task.FromResult(new ValidationResult());
            }

            public IValidatorDescriptor CreateDescriptor()
            {
                throw new NotSupportedException();
            }

            public bool CanValidateInstancesOfType(Type type)
            {
                throw new NotSupportedException();
            }
        }
    }
}