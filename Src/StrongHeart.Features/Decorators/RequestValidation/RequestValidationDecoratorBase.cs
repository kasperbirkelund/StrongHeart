using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    //[DebuggerStepThrough]
    public abstract class RequestValidationDecoratorBase : DecoratorBase
    {
        protected override Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
        {
            IValidator validator = GetValidator();
            ValidationResult result = validator.Validate(new ValidationContext<TRequest>(request));
            if (!result.IsValid)
            {
                throw new BusinessValidationException(result.Errors);
            }

            return func(request);
        }

        protected abstract IValidator GetValidator();
    }
}