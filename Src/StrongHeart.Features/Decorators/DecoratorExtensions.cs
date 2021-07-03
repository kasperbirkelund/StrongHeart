using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators
{
    public static class DecoratorExtensions
    {
        /// <summary>
        /// Returns the inner-most feature which is not a decorator
        /// </summary>
        public static IQueryFeature<TRequest, TResponse> GetInnerMostFeature<TRequest, TResponse>(this IQueryDecorator<TRequest, TResponse> decorator)
            where TRequest : IRequest
            where TResponse : class, IResponseDto
        {
            IQueryFeature<TRequest, TResponse> inner = decorator.GetInnerFeature();
            while (true)
            {
                var innerDecorator = inner as IQueryDecorator<TRequest, TResponse>;
                if (innerDecorator == null)
                {
                    break;
                }
                inner = innerDecorator.GetInnerFeature();
            }
            return inner;
        }

        public static ICommandFeature<TRequest, TRequestDto> GetInnerMostFeature<TRequest, TRequestDto>(this ICommandDecorator<TRequest, TRequestDto> decorator)
            where TRequest : IRequest<TRequestDto>
            where TRequestDto : IRequestDto
        {
            ICommandFeature<TRequest, TRequestDto> inner = decorator.GetInnerFeature();
            while (true)
            {
                var innerDecorator = inner as ICommandDecorator<TRequest, TRequestDto>;
                if (innerDecorator == null)
                {
                    break;
                }
                inner = innerDecorator.GetInnerFeature();
            }
            return inner;
        }

        public static IEnumerable<ICommandDecorator<TRequest, TRequestDto>> GetDecoratorChain<TRequest, TRequestDto>(this ICommandFeature<TRequest, TRequestDto> feature)
            where TRequest : IRequest<TRequestDto> where TRequestDto : IRequestDto
        {
            return GetDecoratorChainPrivate<ICommandFeature<TRequest, TRequestDto>, ICommandDecorator<TRequest, TRequestDto>>(feature, typeof(ICommandFeature<,>), decorator => decorator.GetInnerFeature());
        }

        public static IEnumerable<IQueryDecorator<TRequest, TResponse>> GetDecoratorChain<TRequest, TResponse>(this IQueryFeature<TRequest, TResponse> feature)
            where TResponse : class, IResponseDto
            where TRequest : IRequest
        {
            return GetDecoratorChainPrivate<IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>>(feature, typeof(IQueryFeature<,>), decorator => decorator.GetInnerFeature());
        }

        private static IEnumerable<TDecorator> GetDecoratorChainPrivate<TFeature, TDecorator>(TFeature feature, Type featureType, Func<TDecorator, TFeature> getFeatureFromDecoratorFunc)
            where TDecorator : class
        {
            TFeature actualFeature = feature;
            while (true)
            {
                var innerDecorator = actualFeature as TDecorator;
                if (innerDecorator == null)
                {
                    break;
                }

                yield return innerDecorator;

                FieldInfo? innerFeature = GetInnerFeature(actualFeature!.GetType(), featureType);
                if (innerFeature == null)
                {
                    yield break;
                }

                actualFeature = getFeatureFromDecoratorFunc(innerDecorator);
            }
        }

        private static FieldInfo? GetInnerFeature(IReflect? type, Type featureType)
        {
            return type?.GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic)
                .SingleOrDefault(x => x.FieldType.GetGenericTypeDefinition() == featureType.GetGenericTypeDefinition());
        }
    }
}