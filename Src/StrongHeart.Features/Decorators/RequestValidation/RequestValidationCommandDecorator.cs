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

        public async Task<Result> Execute(TRequest request)
        {
            if (request == null)
            {
                return Result.ClientError("Request is null");
            }
            if (request.Model == null)
            {
                return Result.ClientError("Model is null");
            }
            Result result = await Invoke(_inner.Execute, request);
            if (Conclusion.IsValid)
            {
                return result;
            }
            return Result.ClientError(Conclusion.ToString());
        }

        public ICommandFeature<TRequest, TDto> GetInnerFeature()
        {
            return _inner;
        }
        
#pragma warning disable 693
        protected override IRequestValidatable<TRequest> GetValidator<TRequest>() => (this.GetInnerMostFeature() as IRequestValidatable<TRequest>)!;
#pragma warning restore 693
    }
}