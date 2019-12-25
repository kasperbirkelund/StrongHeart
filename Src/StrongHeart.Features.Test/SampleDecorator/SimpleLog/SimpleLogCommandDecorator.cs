using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;
using StrongHeart.Features.Decorators.Audit;

namespace StrongHeart.Features.Test.SampleDecorator.SimpleLog
{
    //[DebuggerStepThrough]
    public sealed class SimpleLogCommandDecorator<TRequest, TDto> : SimpleLogDecoratorBase, ICommandFeature<TRequest, TDto>, ICommandDecorator<TRequest, TDto>
        where TRequest : IRequest<TDto>
        where TDto : IRequestDto
    {
        private readonly ICommandFeature<TRequest, TDto> _inner;

        public SimpleLogCommandDecorator(ICommandFeature<TRequest, TDto> inner, ISimpleLog simpleLog) : base(simpleLog)
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