using CSharpFunctionalExtensions;
using StrongHeart.Features.Decorators.Authorization;

namespace StrongHeart.Features.Core
{
    public interface IQueryFeature<in TRequest, TResponse> : IFeature<TRequest, Result<TResponse>>,
        IAuthorizable
        where TResponse : class, IResponseDto
        where TRequest : IRequest
    {
    }
}