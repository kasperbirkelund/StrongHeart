using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.TimeAlert.Features.Queries.TestQuery
{
    public record TestQueryResponse(PersonDto? Item) : IGetSingleItemResponse<PersonDto>;
}