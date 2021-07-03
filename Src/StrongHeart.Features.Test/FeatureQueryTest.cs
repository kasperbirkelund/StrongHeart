using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;
using StrongHeart.Features.Decorators.Authorization;
using StrongHeart.Features.Decorators.ExceptionLogging;
using StrongHeart.Features.Decorators.Filtering;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Decorators.Retry;
using StrongHeart.Features.DependencyInjection;
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
            PipelineExtensionsStub extensions = new();
            using (IServiceScope scope = extensions.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                var result1 = await sut.Execute(new TestQueryRequest(new TestAdminCaller(), true));
                result1.Value.Items.Should().Contain("MyTest");

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
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                var result1 = await sut.Execute(new TestQueryRequest(new TestAdminCaller(), true));
                result1.Value.Items.Should().Contain("MyTest");

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
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                IQueryDecorator<TestQueryRequest, TestQueryResponse>[] decorators = sut.GetDecoratorChain().ToArray();
                decorators[0].Should().BeOfType<ExceptionLoggerQueryDecorator<TestQueryRequest, TestQueryResponse>>();
                decorators[1].Should().BeOfType<FilteringQueryDecorator<TestQueryRequest, TestQueryResponse>>();
                decorators[2].Should().BeOfType<RetryQueryDecorator<TestQueryRequest, TestQueryResponse>>();
                decorators.Length.Should().Be(3);
            }
        }
    }
}