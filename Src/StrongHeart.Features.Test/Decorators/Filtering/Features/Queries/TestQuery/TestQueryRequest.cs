using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.Filtering.Features.Queries.TestQuery
{
    public class TestQueryRequest : IRequest
    {
        public TestQueryRequest(ICaller caller)
        {
            Caller = caller;
        }

        public ICaller Caller { get; }
    }
}