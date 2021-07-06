using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Filtering;
using StrongHeart.Features.Test.Decorators.Filtering.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Helpers;
using Xunit;

namespace StrongHeart.Features.Test.Decorators.Filtering
{
    public class FilterDecoratorTest
    {
        [Fact]
        public async Task GivenAFeatureWithInvalidRequest_WhenInvoked_RequestIsBlocked()
        {
            FilterExtension extension = new FilterExtension();
            using (IServiceScope scope = extension.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();

                var response = await sut.Execute(new TestQueryRequest(new TestAdminCaller()));
                response.Value.Items.Count.Should().Be(2);
            }
        }
    }
}