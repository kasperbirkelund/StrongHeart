using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.ExceptionLogging;
using StrongHeart.Features.Test.Decorators.ExceptionLogger.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Helpers;
using Xunit;

namespace StrongHeart.Features.Test.Decorators.ExceptionLogger
{
    public class ExceptionLoggerDecoratorTest
    {
        [Fact]
        public void GivenAnExceptionThrowingFeature_WhenInvoked_ExceptionIsLogged()
        {
            ExceptionLoggerExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(x => { x.AddSingleton<ILogger<ExceptionLoggerDecoratorBase>, ExceptionLoggerSpy>(); }))
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                Func<Task> func = () => sut.Execute(new TestQueryRequest(new TestAdminCaller()));
                func.Should().ThrowAsync<DivideByZeroException>();

                ExceptionLoggerSpy spy = (ExceptionLoggerSpy) scope.ServiceProvider.GetRequiredService<ILogger<ExceptionLoggerDecoratorBase>>();
                spy.Exceptions.Count.Should().Be(1);
            }
        }
    }
}