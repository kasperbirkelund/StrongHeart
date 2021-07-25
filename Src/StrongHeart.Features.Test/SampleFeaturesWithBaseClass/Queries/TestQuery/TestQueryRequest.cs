using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeaturesWithBaseClass.Queries.TestQuery
{
    public record TestQueryRequest(ICaller Caller, bool ShouldSucceed) : IRequest;
}