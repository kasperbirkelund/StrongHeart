using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.TimeAlert;

namespace StrongHeart.Features.Test.Decorators.TimeAlert.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, ITimeAlert
    {
        public async Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            await Task.Delay(request.TimeToExecuteOnSeconds * 1000);
            return Result<TestQueryResponse>.Success(new TestQueryResponse(new PersonDto("PersonA")));
        }

        public TimeSpan MaxAllowedExecutionTime => TimeSpan.FromMilliseconds(200);
    }
}