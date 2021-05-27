using StrongHeart.Features.Decorators.Authorization;

namespace StrongHeart.Features.Core
{
    public interface IQueryFeature<in TRequest, TResponse> : IFeature<TRequest, Result<TResponse>>
        where TResponse : class, IResponseDto
        where TRequest : IRequest
    {
    }
}