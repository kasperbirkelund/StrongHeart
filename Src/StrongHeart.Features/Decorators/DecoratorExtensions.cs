using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators
{
    internal static class DecoratorExtensions
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

        public static ICommandFeature<TRequest, TDto> GetInnerMostFeature<TRequest, TDto>(this ICommandDecorator<TRequest, TDto> decorator)
            where TRequest : IRequest<TDto> 
            where TDto : IRequestDto
        {
            ICommandFeature<TRequest, TDto> inner = decorator.GetInnerFeature();
            while (true)
            {
                var innerDecorator = inner as ICommandDecorator<TRequest, TDto>;
                if (innerDecorator == null)
                {
                    break;
                }
                inner = innerDecorator.GetInnerFeature();
            }
            return inner;
        }

        public static IEventHandlerFeature<TEvent> GetInnerMostFeature<TEvent>(this IEventHandlerDecorator<TEvent> decorator) 
            where TEvent : class, IEvent
        {
            IEventHandlerFeature<TEvent> inner = decorator.GetInnerFeature();
            while (true)
            {
                var innerDecorator = inner as IEventHandlerDecorator<TEvent>;
                if (innerDecorator == null)
                {
                    break;
                }
                inner = innerDecorator.GetInnerFeature();
            }
            return inner;
        }
    }
}