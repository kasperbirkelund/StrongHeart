using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StrongHeart.Features.Decorators.RequestValidation
{
    [Serializable]
    public class InvalidRequestException : BusinessException
    {
        public InvalidRequestException(string message) : base(message, new Dictionary<string, string>())
        {
        }

        protected InvalidRequestException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}