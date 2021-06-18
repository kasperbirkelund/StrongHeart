using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.DependencyInjection;
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
            PipelineExtensionsStub extensions = new();
            using (IServiceScope scope = extensions.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
                IResult result = await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto()));
                result.IsSuccess.Should().BeTrue();

                //extensions.AuditRepoSpy.Audits.Count.Should().Be(1);
                extensions.ExceptionLoggerSpy.Exceptions.Count.Should().Be(0);
            }
        }

        [Fact]
        public async Task TestCustomDecorator()
        {
            SimpleLogSpy logSpy = new();
            SimpleLogExtension extension = new(() => logSpy);
            using (IServiceScope scope = extension.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
                IResult result = await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto()));
                result.IsSuccess.Should().BeTrue();

                logSpy.Messages.Count.Should().Be(2);
            }
        }

        [Fact]
        public async Task TestDefaultDecoratorChain()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddFeatures(x =>
            {
                x.AddDefaultPipeline(() => new ExceptionLoggerSpy());
            }, typeof(FeatureQueryTest).Assembly);
            var provider = services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();



                IResult result = await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto()));
                result.IsSuccess.Should().BeTrue();
            }
        }
    }
}