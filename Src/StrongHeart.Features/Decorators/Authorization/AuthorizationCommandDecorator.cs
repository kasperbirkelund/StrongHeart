using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Authorization
{
    public sealed class AuthorizationCommandDecorator<TRequest, TDto> : AuthorizationDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
        where TRequest : IRequest<TDto>
        where TDto : IRequestDto
    {
        private readonly ICommandFeature<TRequest, TDto> _inner;

        public AuthorizationCommandDecorator(ICommandFeature<TRequest, TDto> inner)
        {
            _inner = inner;
        }

        public override IEnumerable<Claim> GetRequiredClaims()
        {
            return ((IAuthorizable)this.GetInnerMostFeature()).GetRequiredClaims();
        }

        public Task<Result> Execute(TRequest request)
        {
            var requiredClaims = GetRequiredClaims().ToImmutableList();

            if (IsAllowed(request.Caller.Claims, requiredClaims))
            {
                return Invoke(_inner.Execute, request);
            }

            string message = GetExceptionMessage(requiredClaims);
            throw new UnauthorizedAccessException(message);
        }

        public ICommandFeature<TRequest, TDto> GetInnerFeature()
        {
            return _inner;
        }
    }
}