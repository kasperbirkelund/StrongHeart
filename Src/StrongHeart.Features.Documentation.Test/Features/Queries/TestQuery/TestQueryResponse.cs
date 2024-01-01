using StrongHeart.Features.Core;

namespace StrongHeart.Features.Documentation.Test.Features.Queries.TestQuery;

public record TestQueryResponse(PersonDto Item) : IGetSingleItemResponse<PersonDto>;