using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Audit;

namespace StrongHeart.Features.Test.Decorators.Audit.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IAuditable<TestQueryRequest>
    {
        public static Guid FeatureId = new Guid("ba423066-b138-4ad7-9c51-f12e105d62e2");

        public IEnumerable<IRole> GetRequiredRoles() => throw new NotSupportedException();

        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse("Hello")));
        }

        public Func<TestQueryRequest, bool> IsOnBehalfOfOtherSelector => request => false;
        public AuditOptions AuditOptions => new AuditOptions(FeatureId, nameof(TestQueryFeature), logResponse: true );
        public Func<TestQueryRequest, IEnumerable<Guid?>> CorrelationKeySelector => request => new List<Guid?>();
    }
}