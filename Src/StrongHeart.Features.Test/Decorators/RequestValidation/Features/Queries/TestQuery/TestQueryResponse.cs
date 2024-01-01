using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Queries.TestQuery;

public record TestQueryResponse(string? Item) : IGetSingleItemResponse<string>;