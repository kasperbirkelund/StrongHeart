using System.Threading.Tasks;
using FluentValidation;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IRequestValidatable
    {
        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse("Hello")));
        }

        public IValidator GetValidator()
        {
            return new TestQueryRequestValidator();
        }
    }
}