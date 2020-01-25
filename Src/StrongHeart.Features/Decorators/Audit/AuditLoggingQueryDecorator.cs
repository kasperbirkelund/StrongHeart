using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Audit
{
    //[DebuggerStepThrough]
    public sealed class AuditLoggingQueryDecorator<TRequest, TResponse> : AuditLoggingDecoratorBase, IQueryFeature<TRequest, TResponse>, IQueryDecorator<TRequest, TResponse>
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

        public IEnumerable<IRole> GetRequiredRoles()
        {
            return _inner.GetRequiredRoles();
        }

        protected override Guid GetUniqueFeatureId()
        {
            return GetInterface().AuditOptions.UniqueFeatureId;
        }

        public override AuditOptions AuditOptions => GetInterface().AuditOptions;
        public override IEnumerable<Guid?> GetCorrelationKey<T>(T request)
        {
            Func<TRequest, IEnumerable<Guid?>> selector = GetInterface().CorrelationKeySelector;
            return selector(request as TRequest);
        }

        public override bool GetIsOnBehalfOfOther<TRequest1>(TRequest1 request)
        {
            Func<TRequest, bool> selector = GetInterface().IsOnBehalfOfOtherSelector;
            return selector(request as TRequest);
        }

        private IAuditable<TRequest> GetInterface()
        {
            return (IAuditable<TRequest>)this.GetInnerMostFeature();
        }
    }
}