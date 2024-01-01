using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Retry;

public sealed class RetryQueryDecorator<TRequest, TResponse> : RetryDecoratorBase, IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>
    where TRequest : IRequest
    where TResponse : class, IResponseDto
{
    private readonly IQueryFeature<TRequest, TResponse> _inner;

    public RetryQueryDecorator(IQueryFeature<TRequest, TResponse> inner)
    {
        _inner = inner;
    }

    public Task<Result<TResponse>> Execute(TRequest request)
    {
        return Invoke(_inner.Execute, request);
    }

    public IQueryFeature<TRequest, TResponse> GetInnerFeature()
    {
        return _inner;
    }

    protected override IRetryable GetConfig() => (IRetryable)this.GetInnerMostFeature();
}