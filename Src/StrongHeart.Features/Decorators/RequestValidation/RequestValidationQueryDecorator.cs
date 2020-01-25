using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    //[DebuggerStepThrough]
    public sealed class RequestValidationQueryDecorator<TRequest, TResponse> : RequestValidationDecoratorBase, IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : class, IResponseDto
    {
        private readonly IQueryFeature<TRequest, TResponse> _inner;

        public RequestValidationQueryDecorator(IQueryFeature<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public Task<Result<TResponse>> Execute(TRequest request)
        {
            return Invoke(_inner.Execute, request);
        }

        public IEnumerable<IRole> GetRequiredRoles()
        {
            return _inner.GetRequiredRoles();
        }

        public IQueryFeature<TRequest, TResponse> GetInnerFeature()
        {
            return _inner;
        }

        protected override IValidator GetValidator() => (this.GetInnerMostFeature() as IRequestValidatable).GetValidator();
    }
}