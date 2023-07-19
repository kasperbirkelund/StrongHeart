using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public abstract class RequestValidationDecoratorBase : DecoratorBase
    {
        protected ValidationConclusion Conclusion = null!;
        protected override async Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
        {
            IRequestValidator<TRequest> validator = GetValidator<TRequest>();
            ICollection<ValidationMessage> errors = validator.Validate(request).ToList().AsReadOnly();
            Conclusion = new(errors);
            if (Conclusion.IsValid)
            {
                return await func(request);
            }
            return default!;
        }

        protected abstract IRequestValidator<TRequest> GetValidator<TRequest>() where TRequest : IRequest;
    }
}