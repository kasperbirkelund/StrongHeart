using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeaturesWithBaseClass.Command.TestCommand
{
    public class TestCommandFeature : CommandFeatureBase<TestCommandRequest, TestCommandDto>
    {
        public override Task<Result> Execute(TestCommandRequest request)
        {
            Result result = request.Model.ShouldSucceed ? Result.Success() : Result.ServerError("Forced to fail");
            return Task.FromResult(result);
        }
    }
}