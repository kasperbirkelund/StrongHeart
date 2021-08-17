using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongHeart.Features.Decorators.Filtering
{
    public class FilterExtension : IPipelineExtension
    {
        public Func<Type, bool> ShouldApplyPipelineExtension => serviceType => serviceType.DoesImplementInterface(typeof(IFilterable<>));
        public Type QueryTypeDecorator => typeof(FilteringQueryDecorator<,>);
        public Type CommandTypeDecorator => throw new NotSupportedException("Result filtering on commands is not supported.");
        public Type EventHandlerDecorator => throw new NotSupportedException("Result filtering on EventHandlers is not supported.");

        public void RegisterServices(IServiceCollection services)
        {
            //NO OP
        }
    }
}