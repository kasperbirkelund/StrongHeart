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

        public async Task<Result<TResponse>> Execute(TRequest request)
        {
            Result<TResponse> result = await Invoke(_inner.Execute, request);
            if (Conclusion.IsValid)
            {
                return result;
            }
            return Result<TResponse>.ClientError(Conclusion.ToString());
        }

        public IQueryFeature<TRequest, TResponse> GetInnerFeature()
        {
            return _inner;
        }

        protected override IRequestValidatable<TRequest> GetValidator<TRequest>() => this.GetInnerMostFeature() as IRequestValidatable<TRequest>;
    }
}