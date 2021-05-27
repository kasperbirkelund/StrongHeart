using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Documentation.Features.Queries.TestQuery
{
    public class TestQueryResponse : IGetSingleItemResponse<PersonDto>
    {
        public TestQueryResponse(PersonDto item)
        {
            Item = item;
        }

        public PersonDto? Item { get; }
    }
}