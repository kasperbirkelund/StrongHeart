using System.Collections.Generic;
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
        public void TestDefaultDecoratorChain()
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