﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Decorators;
using static System.Linq.Enumerable;

namespace StrongHeart.Features.DependencyInjection
{
    public delegate Type? SpecialTypeHandler(Type interfaceType, Type serviceType);
    public static class FeatureSetupExtensions
    {
        public static IServiceCollection AddStrongHeart(this IServiceCollection services, Action<FeatureSetupOptions> optionsAction, SpecialTypeHandler? specialTypeHandler = null, params Assembly[] assemblies)
        {
            if (!assemblies.Any())
            {
                throw new ArgumentException("Please provide assemblies which contains features to wire up");
            }

            FeatureSetupOptions options = new(services);
            optionsAction(options);

            List<Type> featureTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetInterfaces().Any(Extensions.IsFeatureInterface))
                .ToList();

            foreach (Type featureType in featureTypes)
            {
                AddFeatureRegistration(services, featureType, options, specialTypeHandler);
            }

            return services;
        }

        private static void AddFeatureRegistration(IServiceCollection services, Type type, FeatureSetupOptions options, SpecialTypeHandler? specialTypeHandler)
        {
            Type interfaceType = type.GetInterfaces().Single(Extensions.IsFeatureInterface);

            List<Type> pipeline = new();

            IEnumerable<Type> types = new[] { type }
                .SelectMany(GetDecorators)
                .Concat(GetMandatoryDecorators(interfaceType, type, options))
                .Concat(new[] { type })
                .Reverse();

            pipeline.AddRange(types);

            Func<IServiceProvider, object?> factory = BuildPipeline(pipeline, interfaceType, specialTypeHandler);

            services.AddTransient(interfaceType, factory);
        }

        private static IEnumerable<Type> GetMandatoryDecorators(Type interfaceType, Type serviceType, FeatureSetupOptions options)
        {
            //the first returned decorator will become the outer-most in the pipeline
            if (interfaceType.IsQueryFeatureInterface())
            {
                return GetDecoratorChain(options.Extensions, x => x.QueryTypeDecorator, serviceType);
            }
            else if (interfaceType.IsCommandFeatureInterface())
            {
                return GetDecoratorChain(options.Extensions, x => x.CommandTypeDecorator, serviceType);
            }
            else if (interfaceType.IsEventHandlerFeatureInterface())
            {
                return GetDecoratorChain(options.Extensions, x => x.EventHandlerDecorator, serviceType);
            }
            else
            {
                return Empty<Type>();
            }
        }

        private static IEnumerable<Type> GetDecoratorChain(IEnumerable<IPipelineExtension> extensions, Func<IPipelineExtension, Type> selector, Type serviceType)
        {
            foreach (IPipelineExtension extension in extensions)
            {
                if (extension.ShouldApplyPipelineExtension(serviceType))
                {
                    yield return selector(extension);
                }
            }
        }

        private static Func<IServiceProvider, object?> BuildPipeline(IEnumerable<Type> pipeline, Type interfaceType, SpecialTypeHandler? specialTypeHandler = null)
        {
            IEnumerable<ConstructorInfo> constructors = pipeline
                .SelectMany(x =>
                {
                    Type? t = specialTypeHandler?.Invoke(interfaceType, x);
                    if (t == null)
                    {
                        t = x.IsGenericType ? x.MakeGenericType(interfaceType.GenericTypeArguments) : x;
                    }
                    return t.GetConstructors();
                }).ToList();

            return provider => ObjectFactory(provider, constructors);
        }

        private static object? ObjectFactory(IServiceProvider provider, IEnumerable<ConstructorInfo> constructors)
        {
            object? current = null;
            foreach (ConstructorInfo ctor in constructors)
            {
                List<ParameterInfo> parameterInfos = ctor.GetParameters().ToList();
                object?[] parameters = GetParameters(parameterInfos, current, provider);
                current = ctor.Invoke(parameters);
            }

            return current;
        }

        private static object?[] GetParameters(IList<ParameterInfo> parameterInfos, object? current, IServiceProvider provider)
        {
            var result = new object?[parameterInfos.Count];

            for (int i = 0; i < parameterInfos.Count; i++)
            {
                result[i] = GetParameter(parameterInfos[i], current, provider);
            }

            return result;
        }

        private static object? GetParameter(ParameterInfo parameterInfo, object? current, IServiceProvider provider)
        {
            Type parameterType = parameterInfo.ParameterType;

            if (parameterType.IsCommandFeatureInterface() || parameterType.IsQueryFeatureInterface())
            //if (parameterType.IsFeatureInterface())
            {
                return current;
            }

            object service = provider.GetService(parameterType);
            if (service != null)
            {
                return service;
            }

            throw new ArgumentException($"Type {parameterType} not found");
        }

        private static IEnumerable<Type> GetDecorators(Type serviceType)
        {
            yield break;
        }
    }
}