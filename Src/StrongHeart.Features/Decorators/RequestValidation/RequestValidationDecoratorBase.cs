using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public abstract class RequestValidationDecoratorBase : DecoratorBase
    {
        protected override async Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
        {
            IRequestValidatable<TRequest> validator = GetValidator<TRequest>();
            ICollection<ValidationMessage> errors = validator.ValidationFunc()(request);
            ValidationConclusion result = new(errors);
            if (!result.IsValid)
            {
                throw new BusinessValidationException(result.Messages);
            }

            return await func(request);
        }

        protected abstract IRequestValidatable<TRequest> GetValidator<TRequest>() where TRequest : IRequest;
    }
}