using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;
using StrongHeart.Features.Decorators.Authorization;
using StrongHeart.Features.Decorators.ExceptionLogging;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Decorators.Retry;
using StrongHeart.Features.DependencyInjection;
using StrongHeart.Features.Test.Helpers;
using StrongHeart.Features.Test.SampleDecorator.SimpleLog;
using StrongHeart.Features.Test.SampleFeatures.Command.TestCommand;
using Xunit;

namespace StrongHeart.Features.Test
{
    public class FeatureCommandTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task TestFullFeatureWithFullPipeline(bool shouldSucceed)
        {
            PipelineExtensionsStub extensions = new();
            using (IServiceScope scope = extensions.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
                IResult result = await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto(shouldSucceed)));
                result.IsSuccess.Should().Be(shouldSucceed);

                //extensions.AuditRepoSpy.Audits.Count.Should().Be(1);
                extensions.ExceptionLoggerSpy.Exceptions.Count.Should().Be(0);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task TestCustomDecorator(bool shouldSucceed)
        {
            SimpleLogSpy logSpy = new();
            SimpleLogExtension extension = new(() => logSpy);
            using (IServiceScope scope = extension.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
                IResult result = await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto(shouldSucceed)));
                result.IsSuccess.Should().Be(shouldSucceed);

                logSpy.Messages.Count.Should().Be(2);
            }
        }

        [Fact]
        public void EnsureDefaultDecoratorChainOrder()
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
                ICommandDecorator<TestCommandRequest, TestCommandDto>[] decorators = sut.GetDecoratorChain().ToArray();
                decorators[0].Should().BeOfType<ExceptionLoggerCommandDecorator<TestCommandRequest, TestCommandDto>>();
                decorators[1].Should().BeOfType<AuthorizationCommandDecorator<TestCommandRequest, TestCommandDto>>();
                decorators[2].Should().BeOfType<RequestValidationCommandDecorator<TestCommandRequest, TestCommandDto>>();
                decorators[3].Should().BeOfType < RetryCommandDecorator<TestCommandRequest, TestCommandDto>>();
                decorators.Length.Should().Be(4);
            }
        }
    }
}