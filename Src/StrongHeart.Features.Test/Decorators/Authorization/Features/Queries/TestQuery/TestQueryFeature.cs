using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;
using StrongHeart.Features.Test.Helpers;

namespace StrongHeart.Features.Test.Decorators.Authorization.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>
    {
        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse(new PersonDto("PersonA"))));
        }

        public IEnumerable<IRole> GetRequiredRoles()
        {
            yield return TestRole.Instance;

        }
    }
}