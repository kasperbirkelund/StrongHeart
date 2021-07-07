using System.Collections.Generic;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.ExtensionAlgorithms;

namespace StrongHeart.Features.Decorators
{
    public static class DecoratorExtensions
    {
        public static IQueryFeature<TRequest, TResponse> GetInnerMostFeature<TRequest, TResponse>(this IQueryDecorator<TRequest, TResponse> decorator)
            where TRequest : IRequest
            where TResponse : class, IResponseDto
        {
            return GetInnerMostFeatureAlgorithm.GetInnerMostFeature(decorator, d => d.GetInnerFeature());
        }
        
        public static ICommandFeature<TRequest, TRequestDto> GetInnerMostFeature<TRequest, TRequestDto>(this ICommandDecorator<TRequest, TRequestDto> decorator)
            where TRequest : IRequest<TRequestDto>
            where TRequestDto : IRequestDto
        {
            return GetInnerMostFeatureAlgorithm.GetInnerMostFeature(decorator, d => d.GetInnerFeature());
        }
        
        public static IEnumerable<ICommandDecorator<TRequest, TRequestDto>> GetDecoratorChain<TRequest, TRequestDto>(this ICommandFeature<TRequest, TRequestDto> feature)
            where TRequest : IRequest<TRequestDto> where TRequestDto : IRequestDto
        {
            return GetDecoratorChainAlgorithm.GetDecoratorChain<ICommandFeature<TRequest, TRequestDto>, ICommandDecorator<TRequest, TRequestDto>>(feature, typeof(ICommandFeature<,>), decorator => decorator.GetInnerFeature());
        }

        public static IEnumerable<IQueryDecorator<TRequest, TResponse>> GetDecoratorChain<TRequest, TResponse>(this IQueryFeature<TRequest, TResponse> feature)
            where TResponse : class, IResponseDto
            where TRequest : IRequest
        {
            return GetDecoratorChainAlgorithm.GetDecoratorChain<IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>>(feature, typeof(IQueryFeature<,>), decorator => decorator.GetInnerFeature());
        }
    }
}