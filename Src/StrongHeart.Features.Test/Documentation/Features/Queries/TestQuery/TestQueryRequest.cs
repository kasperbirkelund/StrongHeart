using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Documentation.Features.Queries.TestQuery
{
    public record TestQueryRequest(ICaller Caller) : IRequest;
}