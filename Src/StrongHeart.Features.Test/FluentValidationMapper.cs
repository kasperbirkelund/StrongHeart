using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using FluentValidation.Results;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test;

public static class FluentValidationMapper
{
    public static ICollection<ValidationMessage> Map(ValidationResult result)
    {
        var errors = result.Errors.Select(x => new ValidationMessage(x.ToString())).ToImmutableArray();
        return errors;
    }
}