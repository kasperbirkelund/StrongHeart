using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Retry;

public sealed class RetryCommandDecorator<TRequest, TDto> : RetryDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
    where TRequest : IRequest<TDto>
    where TDto : IRequestDto
{
    private readonly ICommandFeature<TRequest, TDto> _inner;

    public RetryCommandDecorator(ICommandFeature<TRequest, TDto> inner)
    {
        _inner = inner;
    }

    public Task<Result> Execute(TRequest request)
    {
        return Invoke(_inner.Execute, request);
    }

    public ICommandFeature<TRequest, TDto> GetInnerFeature()
    {
        return _inner;
    }

    protected override IRetryable GetConfig() => (IRetryable)this.GetInnerMostFeature();
}