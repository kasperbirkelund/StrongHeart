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
        public void GivenAQueryFeatureWithInvalidRequest_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<IQueryFeature<TestQueryRequest, TestQueryResponse>>();

                Func<Task> func = () => sut.Execute(new TestQueryRequest(new TestAdminCaller(), thisMustNotBeNull: null));
                func.Should().Throw<BusinessValidationException>();
            }
        }
        
        [Fact]
        public void GivenACommandFeatureWithInvalidRequest_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();

                Func<Task> func = () => sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto(-1, birthDay: DateTime.Now)));
                func.Should().Throw<BusinessValidationException>();
            }
        }

        [Fact]
        public void GivenACommandFeatureWithInvalidRequestWithValidationDependency_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();

                Func<Task> func = () => sut.Execute(new TestCommandRequest(new TestAdminCaller(), new TestCommandDto(20, birthDay: 2.May(1990))));
                func.Should().Throw<BusinessValidationException>();
            }
        }

        [Fact]
        public void GivenACommandFeatureWithNullModel_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();

                Func<Task> func = () => sut.Execute(new TestCommandRequest(new TestAdminCaller(), null));
                func.Should().Throw<InvalidRequestException>();
            }
        }

        [Fact]
        public void GivenACommandFeatureWithNullRequest_WhenInvoked_RequestIsBlocked()
        {
            RequestValidatorExtension extension = new();
            using (IServiceScope scope = extension.CreateScope(AddServices))
            {
                var sut = scope.ServiceProvider.GetRequiredService<ICommandFeature<TestCommandRequest, TestCommandDto>>();

                Func<Task> func = () => sut.Execute(null);
                func.Should().Throw<InvalidRequestException>();
            }
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddTransient<IValidationDependency, ValidationDependency>();
            services.AddTransient<TestCommandRequestValidator>();
        }
    }
}