using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Queries.TestQuery
{
    public class TestQueryResponse : IGetSingleItemResponse<string>
    {
        public TestQueryResponse(string item)
        {
            Item = item;
        }

        public string? Item { get; }
    }
}