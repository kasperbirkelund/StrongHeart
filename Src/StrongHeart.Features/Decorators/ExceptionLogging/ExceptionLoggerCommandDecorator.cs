using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Audit;

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

        public Func<TRequest, bool> IsOnBehalfOfOtherSelector => _inner.IsOnBehalfOfOtherSelector;
        public AuditOptions AuditOptions => _inner.AuditOptions;
        public Func<TRequest, IEnumerable<Guid?>> CorrelationKeySelector => _inner.CorrelationKeySelector;

        public ICommandFeature<TRequest, TDto> GetInnerFeature()
        {
            return _inner;
        }

        public IEnumerable<IRole> GetRequiredRoles()
        {
            return _inner.GetRequiredRoles();
        }
    }
}