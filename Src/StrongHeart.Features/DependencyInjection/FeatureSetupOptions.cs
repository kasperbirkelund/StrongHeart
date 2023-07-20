using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Decorators;
using StrongHeart.Features.Decorators.Authorization;
using StrongHeart.Features.Decorators.ExceptionLogging;
using StrongHeart.Features.Decorators.Filtering;
using StrongHeart.Features.Decorators.RequestValidation;
using StrongHeart.Features.Decorators.Retry;
using StrongHeart.Features.Decorators.TimeAlert;

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

        public FeatureSetupOptions AddDefaultPipeline()
        {
            AddPipelineExtensions(new DefaultPipelineExtensions());
            return this;
        }

        private class DefaultPipelineExtensions : List<IPipelineExtension>
        {
            public DefaultPipelineExtensions()
            {
                //First = outermost
                Add(new ExceptionLoggerExtension()); //1: we want to have exception handling outermost to ensure that everything gets logged
                Add(new AuthorizationExtension()); //2: The user must be authorized before we expose any further 
                Add(new TimeAlertExtension()); //3: Ensure that time is monitored
                Add(new RequestValidatorExtension()); //4: Now it is time for input validation
                Add(new FilterExtension()); //5: Filtering only applies to queries
                Add(new RetryExtension()); //6: it makes sense to have retry closest to the real feature
            }
        }
    }
}