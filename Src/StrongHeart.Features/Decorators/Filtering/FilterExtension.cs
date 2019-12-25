using System;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.DependencyInjection;

namespace StrongHeart.Features.Decorators.Filtering
{
    public class FilterExtension : IPipelineExtension
    {
        public Func<Type, bool> ShouldApplyPipelineExtension => serviceType => serviceType.DoesImplementInterface(typeof(IQueryFeature<,>));
        public Type QueryTypeDecorator => typeof(FilteringQueryDecorator<,>);
        public Type CommandTypeDecorator => throw new NotSupportedException();
        public void RegisterServices(IServiceCollection services)
        {
        }
    }
}