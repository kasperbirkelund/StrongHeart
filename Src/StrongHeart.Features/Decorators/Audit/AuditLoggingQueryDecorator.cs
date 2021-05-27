using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Audit
{
    //[DebuggerStepThrough]
    public sealed class AuditLoggingQueryDecorator<TRequest, TResponse> : AuditLoggingDecoratorBase, 
        IQueryFeature<TRequest, TResponse>, 
        IQueryDecorator<TRequest, TResponse>
        where TRequest : class, IRequest
        where TResponse : class, IResponseDto
    {
        private readonly IQueryFeature<TRequest, TResponse> _inner;

        public AuditLoggingQueryDecorator(IQueryFeature<TRequest, TResponse> inner, IFeatureAuditRepository repository) : base(repository)
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

        protected override Guid GetUniqueFeatureId()
        {
            return GetAuditableFeature().AuditOptions.UniqueFeatureId;
        }

        private IAuditable<TRequest> GetAuditableFeature()
        {
            return (this.GetInnerMostFeature() as IAuditable<TRequest>)!;
        }

        public override AuditOptions AuditOptions => GetAuditableFeature().AuditOptions;
        public override IEnumerable<Guid?> GetCorrelationKey<T>(T request)
        {
            Func<TRequest, IEnumerable<Guid?>> selector = GetAuditableFeature().CorrelationKeySelector;
            return selector(request as TRequest);
        }

        public override bool GetIsOnBehalfOfOther<TRequest1>(TRequest1 request)
        {
            Func<TRequest, bool> selector = GetAuditableFeature().IsOnBehalfOfOtherSelector;
            return selector(request as TRequest);
        }
    }
}