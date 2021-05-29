using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeatures.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>
    {
        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            var result = Result<TestQueryResponse>.Success(new TestQueryResponse(new[] { "MyTest" }));
            return Task.FromResult(result);
        }
    }
}