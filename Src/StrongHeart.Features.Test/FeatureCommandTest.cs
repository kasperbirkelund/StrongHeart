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
                List<ICommandDecorator<TestCommandRequest, TestCommandDto>> decorators = sut.GetDecoratorChain().ToList();
                //this test does not ensure the order of the decorators
                decorators.Should()
                    .ContainSingle(x => x is ExceptionLoggerCommandDecorator<TestCommandRequest, TestCommandDto>)
                    .And.ContainSingle(x => x is AuthorizationCommandDecorator<TestCommandRequest, TestCommandDto>)
                    .And.ContainSingle(x => x is RequestValidationCommandDecorator<TestCommandRequest, TestCommandDto>)
                    .And.ContainSingle(x => x is RetryCommandDecorator<TestCommandRequest, TestCommandDto>);
                decorators.Count.Should().Be(4);
            }
        }
    }
}