using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators.Retry
{
    public class RetryExtension : IPipelineExtension
    {
        public Func<Type, bool> ShouldApplyPipelineExtension => serviceType => serviceType.DoesImplementInterface(typeof(IRetryable));
        public Type QueryTypeDecorator => typeof(RetryQueryDecorator<,>);
        public Type CommandTypeDecorator => typeof(RetryCommandDecorator<,>);
        public Type EventHandlerDecorator => throw new NotSupportedException("Retry on EventHandlers is not supported.");

        public void RegisterServices(IServiceCollection services)
        {
            //NO OP
        }
    }
}