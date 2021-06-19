using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.Authorization.Features.Queries.TestQuery
{
    public record TestQueryResponse(PersonDto Item) : IGetSingleItemResponse<PersonDto>;
}