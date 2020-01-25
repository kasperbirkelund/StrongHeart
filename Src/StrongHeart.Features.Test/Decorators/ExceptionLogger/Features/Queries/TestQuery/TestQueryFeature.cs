using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.ExceptionLogger.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>
    {
        public IEnumerable<IRole> GetRequiredRoles()
        {
            //yield break;
            throw new NotSupportedException();
        } 

        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            throw new NotSupportedException();
        }
    }
}