using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public sealed class RequestValidationCommandDecorator<TRequest, TDto> : RequestValidationDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
        where TRequest : IRequest<TDto>
        where TDto : IRequestDto
    {
        private readonly ICommandFeature<TRequest, TDto> _inner;

        public RequestValidationCommandDecorator(ICommandFeature<TRequest, TDto> inner)
        {
            _inner = inner;
        }

        public Task<Result> Execute(TRequest request)
        {
            if (request == null)
            {
                throw new InvalidRequestException("Request is null");
            }
            if (request.Model == null)
            {
                throw new InvalidRequestException("Model is null");
            }
            return Invoke(_inner.Execute, request);
        }

        public ICommandFeature<TRequest, TDto> GetInnerFeature()
        {
            return _inner;
        }
        
        protected override IRequestValidatable<TRequest> GetValidator<TRequest>() => this.GetInnerMostFeature() as IRequestValidatable<TRequest>;
    }
}