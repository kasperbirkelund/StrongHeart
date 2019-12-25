using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Decorators;
using StrongHeart.Features.DependencyInjection;

namespace StrongHeart.Features.Test.Helpers
{
    public static class Extensions
    {
        public static IServiceScope CreateScope(this IList<IPipelineExtension> extensions)
        {
            IServiceCollection col = new ServiceCollection();

            col.AddFeatures(x => { x.AddPipelineExtensions(extensions); }, typeof(FeatureQueryTest).Assembly);
            var provider = col.BuildServiceProvider();
            return provider.CreateScope();
        }

        public static IServiceScope CreateScope(this IPipelineExtension extension)
        {
            IServiceCollection col = new ServiceCollection();

            col.AddFeatures(x => { x.AddPipelineExtension(extension); }, typeof(FeatureQueryTest).Assembly);
            var provider = col.BuildServiceProvider();
            return provider.CreateScope();
        }
    }
}