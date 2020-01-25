using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Audit;

namespace StrongHeart.Features.Test.SampleFeatures.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IAuditable<TestQueryRequest>
    {
        public IEnumerable<IRole> GetRequiredRoles()
        {
            yield break;
        }

        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            var result = Result<TestQueryResponse>.Success(new TestQueryResponse("MyTest"));
            return Task.FromResult(result);
        }

        public Func<TestQueryRequest, bool> IsOnBehalfOfOtherSelector => request => false;
        public AuditOptions AuditOptions => new AuditOptions(Guid.NewGuid(), "Test", false);
        public Func<TestQueryRequest, IEnumerable<Guid?>> CorrelationKeySelector => request => new List<Guid?>(); 
    }
}