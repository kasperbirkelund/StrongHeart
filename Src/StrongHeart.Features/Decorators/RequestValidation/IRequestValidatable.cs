using FluentValidation;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public interface IRequestValidatable
    {
        IValidator GetValidator();
    }
}
