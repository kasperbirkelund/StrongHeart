using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Retry;
using StrongHeart.Features.Test.Decorators.Retry.Features.Command.TestCommand;
using StrongHeart.Features.Test.Decorators.Retry.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Helpers;
using Xunit;

namespace StrongHeart.Features.Test.Decorators.Retry;

public class RetryDecoratorTest
{
    [Fact]
    public async Task RetryCommandEnsuresProperRetry()
    {
        RetryExtension extension = new();
        using (IServiceScope scope = extension.CreateScope(collection => collection.AddScoped<Messenger>()))
        {
            var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
            await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto(2)));

            var messenger = scope.ServiceProvider.GetRequiredService<Messenger>();
            messenger.Counter.Should().Be(2);
        }
    }

    [Fact]
    public async Task RetryQueryEnsuresProperRetry()
    {
        RetryExtension extension = new();
        using (IServiceScope scope = extension.CreateScope(collection => collection.AddScoped<Messenger>()))
        {
            var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
            await sut.Execute(new TestQueryRequest(new TestAdminCaller()));

            var messenger = scope.ServiceProvider.GetRequiredService<Messenger>();
            messenger.Counter.Should().Be(2);
        }
    }
}