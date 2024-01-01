using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.ExceptionLogger.Features.Queries.TestQuery;

public record TestQueryResponse(string Item) : IGetSingleItemResponse<string>;