using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Authorization;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Decorators.Retry;
using StrongHeart.Features.Decorators.TimeAlert;

namespace StrongHeart.Features.Test.SampleFeatures.Command.TestCommand
{
    public class TestCommandFeature : ICommandFeature<TestCommandRequest, TestCommandDto>, IAuthorizable, IRequestValidatable<TestCommandRequest>, IRetryable, ITimeAlert
    {
        public Task<Result> Execute(TestCommandRequest request)
        {
            Result result = request.Model.ShouldSucceed ? Result.Success() : Result.ServerError("Forced to fail");
            return Task.FromResult(result);
        }

        public IEnumerable<Claim> GetRequiredClaims()
        {
            yield break;
        }

        public bool WhenExceptionIsThrownShouldIRetry(Exception exception, int currentAttempt)
        {
            return false;
        }

        public TimeSpan MaxAllowedExecutionTime => TimeSpan.FromSeconds(1);
        public IEnumerable<ValidationMessage> Validate(TestCommandRequest request)
        {
            return FluentValidationMapper.Map(new InlineValidator<TestCommandRequest>().Validate(request));
        }
    }
}