﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Authorization;
using StrongHeart.Features.Test.Helpers;

namespace StrongHeart.Features.Test.Decorators.Authorization.Features.Queries.TestQuery;

public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IAuthorizable
{
    public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
    {
        return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse(new PersonDto("PersonA"))));
    }

    public IEnumerable<Claim> GetRequiredClaims()
    {
        yield return TestClaim.Instance;
    }
}