using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeatures.Queries.TestQuery
{
    public record TestQueryRequest(ICaller Caller) : IRequest;
}