namespace StrongHeart.Features.Core
{
    public interface ICommandFeature<in TRequest, in TRequestDto> : IFeature<TRequest, Result>
        where TRequest : IRequest<TRequestDto>
        where TRequestDto : IRequestDto
    {
    }
}