namespace StrongHeart.Features.Decorators.RequestValidation
{
    public class ValidationMessage
    {
        public string Message { get; }

        public ValidationMessage(string message)
        {
            Message = message;
        }

        public static implicit operator ValidationMessage(string message)
        {
            return new(message);
        }
    }
}