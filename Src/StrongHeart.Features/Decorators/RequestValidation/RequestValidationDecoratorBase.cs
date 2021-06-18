using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public abstract class RequestValidationDecoratorBase : DecoratorBase
    {
        protected override async Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
        {
            IValidator validator = GetValidator();
            ValidationResult result = await validator.ValidateAsync(new ValidationContext<TRequest>(request));
            if (!result.IsValid)
            {
                throw new BusinessValidationException(result.Errors);
            }

            return await func(request);
        }

        protected abstract IValidator GetValidator();
    }
}