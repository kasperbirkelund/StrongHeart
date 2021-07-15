using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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
            ExceptionLoggerExtension<ExceptionLoggerSpy> extension = new();
            using (IServiceScope scope = extension.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                Func<Task> func = () => sut.Execute(new TestQueryRequest(new TestAdminCaller()));
                func.Should().Throw<DivideByZeroException>();

                ExceptionLoggerSpy spy = (ExceptionLoggerSpy) scope.ServiceProvider.GetRequiredService<IExceptionLogger>();
                spy!.Exceptions.Count.Should().Be(1);
            }
        }
    }
}