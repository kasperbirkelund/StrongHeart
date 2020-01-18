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