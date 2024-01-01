using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Documentation.Test.Features.Queries.TestQuery;

public record TestQueryRequest(ICaller Caller) : IRequest;