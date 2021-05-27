using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.ExceptionLogging
{
    //[DebuggerStepThrough]
    public sealed class ExceptionLoggerCommandDecorator<TRequest, TDto> : ExceptionLoggerDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
        where TRequest : IRequest<TDto>
        where TDto : IRequestDto
    {
        private readonly ICommandFeature<TRequest, TDto> _inner;

        public ExceptionLoggerCommandDecorator(ICommandFeature<TRequest, TDto> inner, IExceptionLogger logger) : base(logger)
        {
            _inner = inner;
        }

        public Task<Result> Execute(TRequest request)
        {
            return Invoke(_inner.Execute, request);
        }

        public ICommandFeature<TRequest, TDto> GetInnerFeature()
        {
            return _inner;
        }
    }
}