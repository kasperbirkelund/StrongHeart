using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.TimeAlert;
using StrongHeart.Features.Test.Decorators.TimeAlert.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Decorators.TimeAlert.Features.Command.TestCommand;
using StrongHeart.Features.Test.Helpers;
using Xunit;
using StrongHeart.Features.Decorators.ExceptionLogging;

namespace StrongHeart.Features.Test.Decorators.TimeAlert
{
    public class TimeAlertDecoratorTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        public async Task TimeAlertCommandEnsuresProperLogs(int executionTime, int messagesInLog)
        {
            TimeAlertExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(x => { x.AddSingleton<ILogger<TimeAlertDecoratorBase>, TimeAlertExceededLoggerSpy>(); }))
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
                await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto(executionTime)));

                TimeAlertExceededLoggerSpy spy = (TimeAlertExceededLoggerSpy)scope.ServiceProvider.GetRequiredService<ILogger<TimeAlertDecoratorBase>>();
                spy.Data.Count.Should().Be(messagesInLog);
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        public async Task TimeAlertQueryEnsuresProperLogs(int executionTime, int messagesInLog)
        {
            TimeAlertExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(x => { x.AddSingleton<ILogger<TimeAlertDecoratorBase>, TimeAlertExceededLoggerSpy>(); }))
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                await sut.Execute(new TestQueryRequest(new TestAdminCaller(), executionTime));

                TimeAlertExceededLoggerSpy spy = (TimeAlertExceededLoggerSpy)scope.ServiceProvider.GetRequiredService<ILogger<TimeAlertDecoratorBase>>();
                spy.Data.Count.Should().Be(messagesInLog);
            }
        }
    }
}