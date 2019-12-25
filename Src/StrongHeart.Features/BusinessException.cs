using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace StrongHeart.Features
{
    [Serializable]
    public class BusinessException : Exception
    {
        public IReadOnlyDictionary<string, string> Properties { get; }

        public BusinessException(string message, IDictionary<string, string> properties) : this(message, properties, null)
        {
        }

        public BusinessException(string message, IDictionary<string, string> properties, Exception inner) : base(message, inner)
        {
            Properties = new ReadOnlyDictionary<string, string>(properties);
        }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}