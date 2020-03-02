using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators.Authorization
{
    public class AuthorizationExtension : IPipelineExtension
    {
        public Func<Type, bool> ShouldApplyPipelineExtension => serviceType => serviceType.DoesImplementInterface(typeof(IAuthorizable));
        public Type QueryTypeDecorator => typeof(AuthorizationQueryDecorator<,>);
        public Type CommandTypeDecorator => typeof(AuthorizationCommandDecorator<,>);
        public Type EventHandlerTypeDecorator => throw new NotSupportedException();

        public void RegisterServices(IServiceCollection services)
        {
        }
    }
}