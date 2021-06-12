using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Decorators;
using StrongHeart.Features.DependencyInjection;

namespace StrongHeart.Features.Test.Helpers
{
    public static class Extensions
    {
        public static IServiceScope CreateScope(this IList<IPipelineExtension> extensions, Action<IServiceCollection>? servicesAction = null)
        {
            IServiceCollection col = new ServiceCollection();

            col.AddFeatures(x =>
            {
                x.AddPipelineExtensions(extensions);
            }, typeof(FeatureQueryTest).Assembly);
            servicesAction?.Invoke(col);
            var provider = col.BuildServiceProvider();
            return provider.CreateScope();
        }

        public static IServiceScope CreateScope(this IPipelineExtension extension, Action<IServiceCollection>? servicesAction = null)
        {
            return CreateScope(new[] {extension}, servicesAction);
        }
    }
}