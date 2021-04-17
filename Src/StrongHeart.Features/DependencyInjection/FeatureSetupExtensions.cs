using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;
using static System.Linq.Enumerable;

namespace StrongHeart.Features.DependencyInjection
{
    public static class FeatureSetupExtensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services, Action<FeatureSetupOptions> optionsAction, params Assembly[] assemblies)
        {
            if (!assemblies.Any())
            {
                throw new ArgumentException("Please provide assemblies which contains features to wire up");
            }

            FeatureSetupOptions options = new FeatureSetupOptions(services);
            optionsAction(options);

            List<Type> handlerTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetInterfaces().Any(IsFeatureInterface))
                .ToList();

            foreach (Type type in handlerTypes)
            {
                AddHandler(services, type, options);
            }

            return services;
        }

        private static void AddHandler(IServiceCollection services, Type type, FeatureSetupOptions options)
        {
            Type interfaceType = type.GetInterfaces().Single(IsFeatureInterface);

            List<Type> pipeline = new List<Type>();

            var types = new[] { type }
                .SelectMany(GetDecorators)
                .Concat(GetMandatoryDecorators(interfaceType, type, options))
                .Concat(new[] { type })
                .Reverse();

            pipeline.AddRange(types);

            Func<IServiceProvider, object?> factory = BuildPipeline(pipeline, interfaceType);

            services.AddTransient(interfaceType, factory);
        }

        private static IEnumerable<Type> GetMandatoryDecorators(Type interfaceType, Type serviceType, FeatureSetupOptions options)
        {
            IEnumerable<T> GetDecorator<T>(IEnumerable<IPipelineExtension> extensions, Func<IPipelineExtension, T> selector)
            {
                foreach (IPipelineExtension extension in options.Extensions)
                {
                    if (extension.ShouldApplyPipelineExtension(serviceType))
                    {
                        yield return selector(extension);
                    }
                }
            }

            //the first returned decorator will become the outer-most in the pipeline
            if (IsQuery(interfaceType))
            {
                return GetDecorator(options.Extensions, x => x.QueryTypeDecorator);
            }
            else if (IsCommand(interfaceType))
            {
                return GetDecorator(options.Extensions, x => x.CommandTypeDecorator);
            }
            //else if (IsEventhandler(interfaceType))
            //{
            //    return GetDecorator(options.Extensions, x => x.EventHandlerTypeDecorator);
            //}
            else
            {
                return Empty<Type>();
            }
        }

        private static Func<IServiceProvider, object?> BuildPipeline(IEnumerable<Type> pipeline, Type interfaceType)
        {
            IEnumerable<ConstructorInfo> constructors = pipeline
                .SelectMany(x =>
                {
                    Type type = x.IsGenericType ? x.MakeGenericType(interfaceType.GenericTypeArguments) : x;
                    return type.GetConstructors();
                });

            object? Func(IServiceProvider provider)
            {
                object? current = null;

                foreach (ConstructorInfo ctor in constructors)
                {
                    List<ParameterInfo> parameterInfos = ctor.GetParameters().ToList();

                    object[] parameters = GetParameters(parameterInfos, current, provider);

                    current = ctor.Invoke(parameters);
                }

                return current;
            }

            return Func;
        }

        private static object[] GetParameters(IList<ParameterInfo> parameterInfos, object? current, IServiceProvider provider)
        {
            var result = new object[parameterInfos.Count];

            for (int i = 0; i < parameterInfos.Count; i++)
            {
                result[i] = GetParameter(parameterInfos[i], current, provider);
            }

            return result;
        }

        private static object? GetParameter(ParameterInfo parameterInfo, object? current, IServiceProvider provider)
        {
            Type parameterType = parameterInfo.ParameterType;

            if (IsFeatureInterface(parameterType))
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

        private static bool IsFeatureInterface(Type type)
        {
            return IsCommand(type) || IsQuery(type)/* || IsEventhandler((type))*/;
        }

        private static bool IsCommand(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(ICommandFeature<,>);
        }

        private static bool IsQuery(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }
            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(IQueryFeature<,>);
        }

        //private static bool IsEventhandler(this Type type)
        //{
        //    if (!type.IsGenericType)
        //    {
        //        return false;
        //    }
        //    Type typeDefinition = type.GetGenericTypeDefinition();
        //    return typeDefinition == typeof(IEventHandlerFeature<>);
        //}
    }
}