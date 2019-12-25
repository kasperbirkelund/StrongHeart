using CSharpFunctionalExtensions;
using StrongHeart.Features.Decorators.Audit;
using StrongHeart.Features.Decorators.Authorization;

namespace StrongHeart.Features.Core
{
    public interface ICommandFeature<in TRequest, in TRequestDto> : IFeature<TRequest, Result>,
        IAuditable<TRequest>,
        IAuthorizable
        where TRequest : IRequest<TRequestDto>
        where TRequestDto : IRequestDto
    {
    }
}