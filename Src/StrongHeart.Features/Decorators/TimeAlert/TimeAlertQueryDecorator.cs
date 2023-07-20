using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.TimeAlert
{
    public sealed class TimeAlertQueryDecorator<TRequest, TResponse> : TimeAlertDecoratorBase, IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : class, IResponseDto
    {
        private readonly IQueryFeature<TRequest, TResponse> _inner;

        public TimeAlertQueryDecorator(IQueryFeature<TRequest, TResponse> inner, ILogger<TimeAlertDecoratorBase> logger) : base(logger)
        {
            _inner = inner;
        }

        public Task<Result<TResponse>> Execute(TRequest request)
        {
            return Invoke(_inner.Execute, request);
        }

        public override TimeSpan GetMaxAllowedExecutionTime()
        {
            return ((ITimeAlert)this.GetInnerMostFeature()).MaxAllowedExecutionTime;
        }

        public IQueryFeature<TRequest, TResponse> GetInnerFeature()
        {
            return _inner;
        }
    }
}