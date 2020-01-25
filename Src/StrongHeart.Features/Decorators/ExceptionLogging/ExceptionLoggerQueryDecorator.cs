using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.ExceptionLogging
{
    //[DebuggerStepThrough]
    public sealed class ExceptionLoggerQueryDecorator<TRequest, TResponse> : ExceptionLoggerDecoratorBase, IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : class, IResponseDto
    {
        private readonly IQueryFeature<TRequest, TResponse> _inner;

        public ExceptionLoggerQueryDecorator(IQueryFeature<TRequest, TResponse> inner, IExceptionLogger logger) 
            : base(logger)
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

        public IEnumerable<IRole> GetRequiredRoles()
        {
            return _inner.GetRequiredRoles();
        }
    }
}