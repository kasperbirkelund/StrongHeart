using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.TimeAlert
{
    public sealed class TimeAlertCommandDecorator<TRequest, TDto> : TimeAlertDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
        where TRequest : IRequest<TDto>
        where TDto : IRequestDto
    {
        private readonly ICommandFeature<TRequest, TDto> _inner;

        public TimeAlertCommandDecorator(ICommandFeature<TRequest, TDto> inner, ITimeAlertExceededLogger logger) : base(logger)
        {
            _inner = inner;
        }

        public override TimeSpan GetMaxAllowedExecutionTime()
        {
            return ((ITimeAlert)this.GetInnerMostFeature()).MaxAllowedExecutionTime;
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