using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Test.Helpers;
using StrongHeart.Features.Test.SampleDecorator.SimpleLog;
using StrongHeart.Features.Test.SampleFeatures.Command.TestCommand;
using Xunit;

namespace StrongHeart.Features.Test
{
    public class FeatureCommandTest
    {
        [Fact]
        public async Task TestFullFeatureWithFullPipeline()
        {
            PipelineExtensionsStub extensions = new PipelineExtensionsStub();
            using IServiceScope scope = extensions.CreateScope();

            var sut2 = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
            IResult result2 = await sut2.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto()));
            result2.IsSuccess.Should().BeTrue();

            //extensions.AuditRepoSpy.Audits.Count.Should().Be(1);
            extensions.ExceptionLoggerSpy.Exceptions.Count.Should().Be(0);
        }

        [Fact]
        public async Task TestCustomDecorator()
        {
            SimpleLogSpy logSpy = new SimpleLogSpy();
            SimpleLogExtension extension = new SimpleLogExtension(() => logSpy);
            using IServiceScope scope = extension.CreateScope();

            var sut2 = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
            IResult result2 = await sut2.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto()));
            result2.IsSuccess.Should().BeTrue();

            logSpy.Messages.Count.Should().Be(2);
        }
    }
}