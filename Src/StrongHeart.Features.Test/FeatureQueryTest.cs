using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Test.Helpers;
using StrongHeart.Features.Test.SampleDecorator.SimpleLog;
using StrongHeart.Features.Test.SampleFeatures.Queries.TestQuery;
using Xunit;

namespace StrongHeart.Features.Test
{
    public class FeatureQueryTest
    {
        [Fact]
        public async Task TestFullFeatureWithFullPipeline()
        {
            PipelineExtensionsStub extensions = new PipelineExtensionsStub();
            using IServiceScope scope = extensions.CreateScope();

            var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
            var result1 = await sut.Execute(new TestQueryRequest(new TestAdminCaller()));
            result1.Value.Items.Should().Contain("MyTest");

            extensions.AuditRepoSpy.Audits.Count.Should().Be(1);
            extensions.ExceptionLoggerSpy.Exceptions.Count.Should().Be(0);
        }

        [Fact]
        public async Task TestCustomDecorator()
        {
            SimpleLogSpy logSpy = new SimpleLogSpy();
            SimpleLogExtension extension = new SimpleLogExtension(() => logSpy);
            using IServiceScope scope = extension.CreateScope();

            var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
            var result1 = await sut.Execute(new TestQueryRequest(new TestAdminCaller()));
            result1.Value.Items.Should().Contain("MyTest");

            logSpy.Messages.Count.Should().Be(2);
        }
    }
}