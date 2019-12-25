using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
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

        public IEnumerable<IRole> GetRequiredRoles()
        {
            return _inner.GetRequiredRoles();
        }

        protected override Guid GetUniqueFeatureId()
        {
            return AuditOptions.UniqueFeatureId;
        }


        public override AuditOptions AuditOptions => _inner.AuditOptions;
        public override IEnumerable<Guid?> GetCorrelationKey<T>(T request)
        {
            return CorrelationKeySelector(request as TRequest);
        }

        public override bool GetIsOnBehalfOfOther<TRequest1>(TRequest1 request)
        {
            return IsOnBehalfOfOtherSelector(request as TRequest);
        }

        public Func<TRequest, IEnumerable<Guid?>> CorrelationKeySelector => _inner.CorrelationKeySelector;
        public Func<TRequest, bool> IsOnBehalfOfOtherSelector => _inner.IsOnBehalfOfOtherSelector;
    }
}