using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public class BusinessValidationException : Exception
    {
        public IList<ValidationFailure> ResultErrors { get; }

        public BusinessValidationException(IList<ValidationFailure> resultErrors) : base("Validation error")
        {
            ResultErrors = resultErrors;
        }
    }
}
