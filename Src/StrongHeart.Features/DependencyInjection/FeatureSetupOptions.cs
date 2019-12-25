using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Decorators;

namespace StrongHeart.Features.DependencyInjection
{
    public class FeatureSetupOptions
    {
        private readonly IServiceCollection _services;
        internal readonly IList<IPipelineExtension> Extensions = new List<IPipelineExtension>();

        public FeatureSetupOptions(IServiceCollection services)
        {
            _services = services;
        }

        public FeatureSetupOptions AddPipelineExtension<T>(T extension) where T : IPipelineExtension
        {
            extension.RegisterServices(_services);
            Extensions.Add(extension);
            return this;
        }

        public FeatureSetupOptions AddPipelineExtensions<T>(IList<T> list) where T : IPipelineExtension
        {
            foreach (T item in list)
            {
                AddPipelineExtension(item);
            }
            return this;
        }
    }
}