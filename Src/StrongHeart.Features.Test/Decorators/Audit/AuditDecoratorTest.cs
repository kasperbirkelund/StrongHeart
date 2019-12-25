using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Audit;
using StrongHeart.Features.Test.Decorators.Audit.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Helpers;
using Xunit;

namespace StrongHeart.Features.Test.Decorators.Audit
{
    public class AuditDecoratorTest
    {
        [Fact]
        public async Task GivenAFeature_WhenInvoked_AuditIsPerformed()
        {
            FeatureAuditRepositorySpy spy = new FeatureAuditRepositorySpy();

            AuditExtension extension = new AuditExtension(() => spy);
            using IServiceScope scope = extension.CreateScope();
            var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
            var response = await sut.Execute(new TestQueryRequest(new TestAdminCaller()));
            
            spy.Audits.Count.Should().Be(1);
        }
    }
}
