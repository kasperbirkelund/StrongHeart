using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Audit
{
    //[DebuggerStepThrough]
    public sealed class AuditLoggingCommandDecorator<TRequest, TDto> : AuditLoggingDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
        where TRequest : class, IRequest<TDto>
        where TDto : IRequestDto
    {
        private readonly ICommandFeature<TRequest, TDto> _inner;

        public AuditLoggingCommandDecorator(ICommandFeature<TRequest, TDto> inner, IFeatureAuditRepository repository) : base(repository)
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

        protected override Guid GetUniqueFeatureId()
        {
            return AuditOptions.UniqueFeatureId;
        }

        private IAuditable<TRequest> GetAuditableFeature()
        {
            return (this.GetInnerMostFeature() as IAuditable<TRequest>)!;
        }
        
        public override AuditOptions AuditOptions => GetAuditableFeature().AuditOptions;
        public override IEnumerable<Guid?> GetCorrelationKey<T>(T request)
        {
            return CorrelationKeySelector(request as TRequest);
        }

        public override bool GetIsOnBehalfOfOther<TRequest1>(TRequest1 request)
        {
            return IsOnBehalfOfOtherSelector(request as TRequest);
        }

        public Func<TRequest, IEnumerable<Guid?>> CorrelationKeySelector => GetAuditableFeature().CorrelationKeySelector;
        public Func<TRequest, bool> IsOnBehalfOfOtherSelector => GetAuditableFeature().IsOnBehalfOfOtherSelector;
    }
}