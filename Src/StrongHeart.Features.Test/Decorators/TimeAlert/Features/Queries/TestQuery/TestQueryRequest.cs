using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.TimeAlert.Features.Queries.TestQuery
{
    public record TestQueryRequest(ICaller Caller, int TimeToExecuteOnSeconds) : IRequest;
}