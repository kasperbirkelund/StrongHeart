using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.RequestValidation
{
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

        public IQueryFeature<TRequest, TResponse> GetInnerFeature()
        {
            return _inner;
        }
        
        //protected override Func<TRequest, ValidationConclusion> GetValidator<TRequest>() => (this.GetInnerMostFeature() as IRequestValidatable<TRequest>).ValidationFunc();
        protected override IRequestValidatable<TRequest> GetValidator<TRequest>()
        {
            return this.GetInnerMostFeature() as IRequestValidatable<TRequest>;
        }
    }
}