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
            ICommandFeature<TRequest, TRequestDto> actualFeature = feature;
            while (true)
            {
                var innerDecorator = actualFeature as ICommandDecorator<TRequest, TRequestDto>;
                if (innerDecorator == null)
                {
                    break;
                }
                yield return innerDecorator;

                FieldInfo? innerFeature = actualFeature!
                    .GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic)
                    .SingleOrDefault(x => x.FieldType.GetGenericTypeDefinition() == typeof(ICommandFeature<,>).GetGenericTypeDefinition());

                if (innerFeature == null)
                {
                    yield break;
                }
                actualFeature = innerDecorator.GetInnerFeature();
            }
        }

        public static IEnumerable<IQueryDecorator<TRequest, TResponse>> GetDecoratorChain<TRequest, TResponse>(this IQueryFeature<TRequest, TResponse> feature) 
            where TResponse : class, IResponseDto 
            where TRequest : IRequest
        {
            IQueryFeature<TRequest, TResponse> actualFeature = feature;
            while (true)
            {
                var innerDecorator = actualFeature as IQueryDecorator<TRequest, TResponse>;
                if (innerDecorator == null)
                {
                    break;
                }
                yield return innerDecorator;

                FieldInfo? innerFeature = actualFeature!
                    .GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic)
                    .SingleOrDefault(x => x.FieldType.GetGenericTypeDefinition() == typeof(IQueryFeature<,>).GetGenericTypeDefinition());

                if (innerFeature == null)
                {
                    yield break;
                }
                actualFeature = innerDecorator.GetInnerFeature();
            }
        }
    }
}