using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Test.Helpers;
using StrongHeart.Features.Test.SampleDecorator.SimpleLog;
using StrongHeart.Features.Test.SampleFeatures.Command.TestCommand;
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
            result1.Value.Name.Should().Be("MyTest");

            var sut2 = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
            var result2 = await sut2.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto()));
            result2.IsSuccess.Should().BeTrue();

            extensions.AuditRepoSpy.Audits.Count.Should().Be(2);
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
            result1.Value.Name.Should().Be("MyTest");

            var sut2 = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
            var result2 = await sut2.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto()));
            result2.IsSuccess.Should().BeTrue();

            logSpy.Messages.Count.Should().Be(4);
        }
    }
}