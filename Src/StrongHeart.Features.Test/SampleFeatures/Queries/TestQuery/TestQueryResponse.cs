using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeatures.Queries.TestQuery
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