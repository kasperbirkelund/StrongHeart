using System;

namespace StrongHeart.Features.Core
{
    public class EventMetadata 
    {
        public EventMetadata(string messageType, string publisherApplicationName, DateTime enqueueTime, Guid correlationId, string publishedByUserName)
        {
            MessageType = messageType;
            PublisherApplicationName = publisherApplicationName;
            EnqueueTime = enqueueTime;
            CorrelationId = correlationId;
            PublishedByUserName = publishedByUserName;
        }

        public string MessageType { get;  }
        public string PublisherApplicationName { get; }
        public DateTime EnqueueTime { get; }
        public Guid CorrelationId { get;  }
        public string PublishedByUserName { get; }
    }
}