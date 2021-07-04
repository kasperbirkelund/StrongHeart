using System.Collections.Generic;
using System.Linq;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    public class ValidationConclusion
    {
        public ICollection<ValidationMessage> Messages { get; }
        public bool IsValid => !Messages.Any();

        public ValidationConclusion(ICollection<ValidationMessage> messages)
        {
            Messages = messages;
        }
    }

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