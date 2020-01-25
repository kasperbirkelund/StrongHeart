using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Audit;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    //[DebuggerStepThrough]
    public sealed class RequestValidationCommandDecorator<TRequest, TDto> : RequestValidationDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
        where TRequest : IRequest<TDto>
        where TDto : IRequestDto
    {
        private readonly ICommandFeature<TRequest, TDto> _inner;

        public RequestValidationCommandDecorator(ICommandFeature<TRequest, TDto> inner)
        {
            _inner = inner;
        }

        public Task<Result> Execute(TRequest request)
        {
            if (request == null)
            {
                throw new InvalidRequestException("Request is null");
            }
            if (request.Model == null)
            {
                throw new InvalidRequestException("Model is null");
            }
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

        protected override IValidator GetValidator() => (this.GetInnerMostFeature() as IRequestValidatable).GetValidator();
    }
}