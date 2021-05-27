using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Authorization
{
    //[DebuggerStepThrough]
    public sealed class AuthorizationCommandDecorator<TRequest, TDto> : AuthorizationDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
        where TRequest : IRequest<TDto>
        where TDto : IRequestDto
    {
        private readonly ICommandFeature<TRequest, TDto> _inner;

        public AuthorizationCommandDecorator(ICommandFeature<TRequest, TDto> inner)
        {
            _inner = inner;
        }

        public override IEnumerable<IRole> GetRequiredRoles()
        {
            return ((IAuthorizable)this.GetInnerMostFeature()).GetRequiredRoles();
        }

        public Task<Result> Execute(TRequest request)
        {
            var requiredRoles = GetRequiredRoles().ToImmutableList();

            if (IsAllowed(request.Caller.Roles, requiredRoles))
            {
                return Invoke(_inner.Execute, request);
            }

            string message = GetExceptionMessage(requiredRoles);
            throw new UnauthorizedAccessException(message);
        }

        public ICommandFeature<TRequest, TDto> GetInnerFeature()
        {
            return _inner;
        }
    }
}