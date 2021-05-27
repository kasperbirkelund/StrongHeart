using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Decorators.ExceptionLogger.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>
    {
        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            throw new DivideByZeroException();
        }
    }
}