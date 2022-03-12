using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Authorization;
using StrongHeart.Features.Test.Decorators.Authorization.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Helpers;
using Xunit;

namespace StrongHeart.Features.Test.Decorators.Authorization
{
    public class AuthorizationDecoratorTest
    {
        [Fact]
        public void GivenAnFeatureWithRoleRestriction_WhenInvokedWithInsufficientClaims_ExceptionThrown()
        {
            AuthorizationExtension extension = new();
            using (IServiceScope scope = extension.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                Func<Task> func = async () => await sut.Execute(new TestQueryRequest(new TestCustomCaller( /*no custom roles*/)));
                func.Should().ThrowAsync<UnauthorizedAccessException>();
            }
        }

        [Fact]
        public async Task GivenAnFeatureWithRoleRestriction_WhenInvokedWithSufficientClaims_NoExceptions()
        {
            AuthorizationExtension extension = new();
            using (IServiceScope scope = extension.CreateScope())
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                var response = await sut.Execute(new TestQueryRequest(new TestCustomCaller(TestClaim.Instance)));
                response.IsSuccess.Should().BeTrue();
            }
        }
    }
}