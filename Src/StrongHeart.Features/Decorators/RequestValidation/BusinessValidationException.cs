using System;
using System.Collections.Generic;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public class BusinessValidationException : Exception
    {
        public ICollection<ValidationMessage> ResultErrors { get; }

        public BusinessValidationException(ICollection<ValidationMessage> resultErrors) : base("Validation error")
        {
            ResultErrors = resultErrors;
        }
    }
}
