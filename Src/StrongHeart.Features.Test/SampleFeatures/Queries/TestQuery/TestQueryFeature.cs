using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Filtering;
using StrongHeart.Features.Decorators.Retry;

namespace StrongHeart.Features.Test.SampleFeatures.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IRetryable, IFilterable<TestQueryResponse>
    {
        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            Result<TestQueryResponse> result = request.ShouldSucceed ?
                Result<TestQueryResponse>.Success(new TestQueryResponse(new[] { "MyTest" })) :
                Result<TestQueryResponse>.Failure("Forced to fail");

            return Task.FromResult(result);
        }

        public bool WhenExceptionIsThrownShouldIRetry(Exception exception, int currentAttempt)
        {
            return false;
        }

        public TestQueryResponse GetFilteredItem(IFilterDecisionContext context, TestQueryResponse response)
        {
            return response;
        }
    }
}