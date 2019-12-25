using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.ExceptionLogger.Features.Queries.TestQuery
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