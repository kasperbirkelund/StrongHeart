using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeaturesWithBaseClass.Queries.TestQuery
{
    public class TestQueryFeature : QueryFeatureBase<TestQueryRequest, TestQueryResponse>
    {
        public override Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            Result<TestQueryResponse> result = request.ShouldSucceed ?
                Result<TestQueryResponse>.Success(new TestQueryResponse(new[] { "MyTest" })) :
                Result<TestQueryResponse>.ServerError("Forced to fail");

            return Task.FromResult(result);
        }
    }
}