using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Authorization
{
    //[DebuggerStepThrough]
    public sealed class AuthorizationQueryDecorator<TRequest, TResponse> : AuthorizationDecoratorBase, IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : class, IResponseDto
    {
        private readonly IQueryFeature<TRequest, TResponse> _inner;

        public AuthorizationQueryDecorator(IQueryFeature<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public override IEnumerable<IRole> GetRequiredRoles()
        {
            return (this.GetInnerMostFeature()).GetRequiredRoles();
        }

        public Task<Result<TResponse>> Execute(TRequest request)
        {
            var requiredRoles = GetRequiredRoles().ToImmutableList();

            if (IsAllowed(request.Caller.Roles, requiredRoles))
            {
                return Invoke(_inner.Execute, request);
            }

            string message = GetExceptionMessage(requiredRoles);
            throw new UnauthorizedAccessException(message);
        }

        public IQueryFeature<TRequest, TResponse> GetInnerFeature()
        {
            return _inner;
        }
    }
}