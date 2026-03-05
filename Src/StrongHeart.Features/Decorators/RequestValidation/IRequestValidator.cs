namespace StrongHeart.Features.Decorators.RequestValidation;

public interface IRequestValidator<in TRequest>
    where TRequest: IRequest
{
    IEnumerable<ValidationMessage> ValidateRequest(TRequest request);
}