using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test
{
    public static class FluentValidationMapper
    {
        public static ICollection<ValidationMessage> Map(Task<ValidationResult> result)
        {
            //HACK: ought NOT to call this sync.
            var errors = result.GetAwaiter().GetResult().Errors.Select(x => new ValidationMessage(x.ToString())).ToImmutableArray();
            return errors;
        }
    }
}
