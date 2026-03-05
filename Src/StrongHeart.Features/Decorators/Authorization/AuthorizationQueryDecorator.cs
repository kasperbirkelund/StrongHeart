using System.Security.Claims;

namespace StrongHeart.Features.Decorators.Authorization;

public sealed class AuthorizationQueryDecorator<TRequest, TResponse> : AuthorizationDecoratorBase, IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>
    where TRequest : IRequest
    where TResponse : class, IResponseDto
{
    private readonly IQueryFeature<TRequest, TResponse> _inner;

    public AuthorizationQueryDecorator(IQueryFeature<TRequest, TResponse> inner)
    {
        _inner = inner;
    }

    public override IEnumerable<Claim> GetRequiredClaims()
    {
        return (this.GetInnerMostFeature() as IAuthorizable).GetRequiredClaims();
    }

    public Task<Result<TResponse>> Execute(TRequest request)
    {
        return Invoke(_inner.Execute, request);
    }

    public IQueryFeature<TRequest, TResponse> GetInnerFeature()
    {
        return _inner;
    }
}