using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators
{
    public interface ICommandDecorator<in TRequest, TDto>
        where TRequest : IRequest<TDto>
        where TDto : IRequestDto
    {
        ICommandFeature<TRequest, TDto> GetInnerFeature();
    }
}