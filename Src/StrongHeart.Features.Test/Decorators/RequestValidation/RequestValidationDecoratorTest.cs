using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Test.Decorators.RequestValidation.Features.Commands.TestCommand;
using StrongHeart.Features.Test.Decorators.RequestValidation.Features.Queries.TestQuery;
using StrongHeart.Features.Test.Helpers;
using Xunit;

namespace StrongHeart.Features.Test.Decorators.RequestValidation
{
    public class RequestValidationDecoratorTest
    {
        [Fact]
        public async Task GivenAQueryFeatureWithInvalidRequest_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();
                var result = await sut.Execute(new TestQueryRequest(new TestAdminCaller(), ThisMustNotBeNull: null));
                result.IsFailure.Should().BeTrue();
                result.Status.Should().Be(ResultType.ClientError);
                result.Error.Should().Be(@"Validation messages: 
- ThisMustNotBeNull must not be null or empty");
            }
        }

        [Fact(Skip = "Doesn't run on build server :-/")]
        public async Task GivenACommandFeatureWithInvalidRequest_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
                var result = await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto(-1, BirthDay: DateTime.Now)));
                result.IsFailure.Should().BeTrue();
                result.Status.Should().Be(ResultType.ClientError);
                result.Error.Should().Be(@"Validation messages: 
- 'Model Age' skal være større end '0'.
- Only year after 2000 is allowed");
            }
        }

        [Fact]
        public async Task GivenACommandFeatureWithInvalidRequestWithValidationDependency_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
                var result = await sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto(20, BirthDay: 2.May(1990))));
                result.IsFailure.Should().BeTrue();
                result.Status.Should().Be(ResultType.ClientError);
                result.Error.Should().Be(@"Validation messages: 
- Only year after 2000 is allowed");
            }
        }

        [Fact]
        public async Task GivenACommandFeatureWithNullModel_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();
                var result = await sut.Execute(new TestCommandRequest(new TestAdminCaller(), null!));
                result.IsFailure.Should().BeTrue();
                result.Status.Should().Be(ResultType.ClientError);
                result.Error.Should().Be(@"Model is null");
            }
        }

        [Fact]
        public async Task GivenACommandFeatureWithNullRequest_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();

                var result = await sut.Execute(null);
                result.IsFailure.Should().BeTrue();
                result.Status.Should().Be(ResultType.ClientError);
                result.Error.Should().Be(@"Request is null");
            }
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddTransient<IValidationDependency, ValidationDependency>();
            services.AddTransient<TestCommandRequestValidator>();
        }
    }
}