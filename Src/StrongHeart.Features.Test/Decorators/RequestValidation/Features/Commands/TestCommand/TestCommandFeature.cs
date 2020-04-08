using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Audit;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand
{
    public class TestCommandFeature : ICommandFeature<TestCommandRequest, TestCommandDto>, IRequestValidatable
    {
        public Task<Result> Execute(TestCommandRequest request)
        {
            return Task.FromResult(Result.Success());
        }

        public Func<TestCommandRequest, bool> IsOnBehalfOfOtherSelector => throw new NotSupportedException();
        public AuditOptions AuditOptions => throw new NotSupportedException();
        public Func<TestCommandRequest, IEnumerable<Guid?>> CorrelationKeySelector => throw new NotSupportedException();
        public IEnumerable<IRole> GetRequiredRoles()
        {
            yield break;
        }

        public IValidator GetValidator()
        {
            return new TestCommandRequestValidator();
        }
    }
}