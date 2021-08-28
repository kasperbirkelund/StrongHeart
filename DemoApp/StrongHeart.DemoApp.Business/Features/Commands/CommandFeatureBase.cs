using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.DemoApp.Business.Features.Commands
{
    /// <summary>
    /// This class is a project specific class where you can apply your project specific decorators.
    /// Make sure that all your features inherent this base class to be able to make streamlined behaviour
    /// </summary>
    public abstract class CommandFeatureBase<TRequest, TRequestDto> : FeatureBase, ICommandFeature<TRequest, TRequestDto>, IRequestValidatable<TRequest>
        where TRequest : IRequest<TRequestDto>
        where TRequestDto : IRequestDto
    {
        public abstract Task<Result> Execute(TRequest request);
        public Func<TRequest, IEnumerable<ValidationMessage>> ValidationFunc() => ValidateRequest;
        protected abstract IEnumerable<ValidationMessage> ValidateRequest(TRequest request);
    }
}
