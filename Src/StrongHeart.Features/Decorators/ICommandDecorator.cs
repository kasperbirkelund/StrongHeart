using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators
{
    public interface ICommandDecorator<in TRequest, in TRequestDto>
        where TRequest : IRequest<TRequestDto>
        where TRequestDto : IRequestDto
    {
        ICommandFeature<TRequest, TRequestDto> GetInnerFeature();
    }
}