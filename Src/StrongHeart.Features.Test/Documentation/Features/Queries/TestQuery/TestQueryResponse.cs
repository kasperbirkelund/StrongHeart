using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Documentation.Features.Queries.TestQuery
{
    public record TestQueryResponse(PersonDto Item) : IGetSingleItemResponse<PersonDto>;
}