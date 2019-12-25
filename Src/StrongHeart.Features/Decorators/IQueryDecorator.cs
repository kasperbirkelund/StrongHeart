using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators
{
    public interface IQueryDecorator<in TRequest, TResponse> 
        where TRequest : IRequest 
        where TResponse : class, IResponseDto
    {
        IQueryFeature<TRequest, TResponse> GetInnerFeature();
    }
}