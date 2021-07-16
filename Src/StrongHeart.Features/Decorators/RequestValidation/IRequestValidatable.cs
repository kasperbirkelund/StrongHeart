using System;
using System.Collections.Generic;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public interface IRequestValidatable<in TRequest>
        where TRequest: IRequest
    {
        Func<TRequest, IEnumerable<ValidationMessage>> ValidationFunc();
    }
}
