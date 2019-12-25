using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.Audit.Features.Queries.TestQuery
{
    public class TestQueryResponse : IResponseDto
    {
        public TestQueryResponse(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}