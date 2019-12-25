using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IRequestValidatable
    {
        public static Guid FeatureId = new Guid("ba423066-b138-4ad7-9c51-f12e105d62e2");

        public IEnumerable<IRole> GetRequiredRoles() => throw new NotSupportedException();

        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            return Task.FromResult(Result.Success(new TestQueryResponse("Hello")));
        }

        public IValidator GetValidator()
        {
            return new TestQueryRequestValidator();
        }
    }
}