using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;

namespace StrongHeart.Features.Test.SampleDecorator.SimpleLog
{
    public sealed class SimpleLogQueryDecorator<TRequest, TResponse> : SimpleLogDecoratorBase, IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : class, IResponseDto
    {
        private readonly IQueryFeature<TRequest, TResponse> _inner;

        public SimpleLogQueryDecorator(IQueryFeature<TRequest, TResponse> inner, ISimpleLog simpleLog) 
            : base(simpleLog)
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
    }
}