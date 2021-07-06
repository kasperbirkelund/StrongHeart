using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.TimeAlert;
using StrongHeart.Features.Test.Decorators.TimeAlert.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Decorators.TimeAlert.Features.Command.TestCommand;
using StrongHeart.Features.Test.Helpers;
using Xunit;

namespace StrongHeart.Features.Test.Decorators.TimeAlert
{
    public class TimeAlertDecoratorTest
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(3, 1)]
        public async Task TimeAlertCommandEnsuresProperLogs(int executionTime, int messagesInLog)
        {
            TimeAlertExceededLoggerSpy spy = new();
            TimeAlertExtension extension = new(() => spy);
            using (IServiceScope scope = extension.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
                await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto(executionTime)));

                var logger = scope.ServiceProvider.GetRequiredService<ITimeAlertExceededLogger>();
                spy.Data.Count.Should().Be(messagesInLog);
            }
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(3, 1)]
        public async Task TimeAlertQueryEnsuresProperLogs(int executionTime, int messagesInLog)
        {
            TimeAlertExceededLoggerSpy spy = new();
            TimeAlertExtension extension = new(() => spy);
            using (IServiceScope scope = extension.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                await sut.Execute(new TestQueryRequest(new TestAdminCaller(), executionTime));

                var logger = scope.ServiceProvider.GetRequiredService<ITimeAlertExceededLogger>();
                spy.Data.Count.Should().Be(messagesInLog);
            }
        }
    }
}