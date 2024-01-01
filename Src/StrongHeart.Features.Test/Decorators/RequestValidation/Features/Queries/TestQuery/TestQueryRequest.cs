using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Queries.TestQuery;

public record TestQueryRequest(ICaller Caller, string? ThisMustNotBeNull) : IRequest;