﻿using System;
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
        public void GivenAnFeatureWithRoleRestriction_WhenInvokedWithInsufficientRoles_ExceptionThrown()
        {
            AuthorizationExtension extension = new AuthorizationExtension();
            using IServiceScope scope = extension.CreateScope();
            var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
            Func<Task> func = () => sut.Execute(new TestQueryRequest(new TestCustomCaller(/*no custom roles*/)));
            func.Should().Throw<UnauthorizedAccessException>();
        }
    }
}
