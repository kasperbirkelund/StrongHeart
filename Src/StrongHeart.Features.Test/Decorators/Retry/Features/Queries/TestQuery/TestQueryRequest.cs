using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.Retry.Features.Queries.TestQuery
{
    public class TestQueryRequest : IRequest
    {
        public ICaller Caller { get; }

        public TestQueryRequest(ICaller caller)
        {
            Caller = caller;
        }
    }
}