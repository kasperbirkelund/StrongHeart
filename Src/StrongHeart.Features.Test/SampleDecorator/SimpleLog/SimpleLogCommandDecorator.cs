using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;

namespace StrongHeart.Features.Test.SampleDecorator.SimpleLog
{
    public sealed class SimpleLogCommandDecorator<TRequest, TDto> : SimpleLogDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
        where TRequest : IRequest<TDto>
        where TDto : IRequestDto
    {
        private readonly ICommandFeature<TRequest, TDto> _inner;

        public SimpleLogCommandDecorator(ICommandFeature<TRequest, TDto> inner, ISimpleLog simpleLog) : base(simpleLog)
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