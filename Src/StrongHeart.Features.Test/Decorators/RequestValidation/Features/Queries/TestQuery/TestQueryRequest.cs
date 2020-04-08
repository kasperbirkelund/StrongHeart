using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Queries.TestQuery
{
    public class TestQueryRequest : IRequest
    {
        public TestQueryRequest(ICaller caller, string? thisMustNotBeNull)
        {
            Caller = caller;
            ThisMustNotBeNull = thisMustNotBeNull;
        }

        public ICaller Caller { get; }
        public string? ThisMustNotBeNull { get; }
    }
}