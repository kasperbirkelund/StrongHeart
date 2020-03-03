using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
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
        public async Task GivenAQueryResultSuccessFeature_WhenInvoked_AuditIsPerformed()
        {
            FeatureAuditRepositorySpy spy = new FeatureAuditRepositorySpy();

            AuditExtension extension = new AuditExtension(() => spy);
            using IServiceScope scope = extension.CreateScope();
            var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
            await sut.Execute(new TestQueryRequest(new TestAdminCaller(), shouldReturnResultFailure: false));

            using (new AssertionScope())
            {
                spy.Audits.Count.Should().Be(1);
                var actual = spy.Audits.Single();
                actual.Status.Should().Be(FeatureAuditStatus.Success);
            }
        }

        [Fact]
        public async Task GivenAQueryResultFailureFeature_WhenInvoked_AuditIsPerformed()
        {
            FeatureAuditRepositorySpy spy = new FeatureAuditRepositorySpy();

            AuditExtension extension = new AuditExtension(() => spy);
            using IServiceScope scope = extension.CreateScope();
            var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
            await sut.Execute(new TestQueryRequest(new TestAdminCaller(), shouldReturnResultFailure: true));

            using (new AssertionScope())
            {
                spy.Audits.Count.Should().Be(1);
                var actual = spy.Audits.Single();
                actual.Status.Should().Be(FeatureAuditStatus.ResultFailure);
            }
        }
    }
}