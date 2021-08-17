namespace StrongHeart.Features.Core.Events
{
    public class EventMessage<TEvent, TMetadata>
        where TEvent : class, IEvent
        where TMetadata : class, IMetadata
    {
        public EventMessage(TMetadata metadata, TEvent body)
        {
            Metadata = metadata;
            Body = body;
        }

        public TMetadata Metadata { get; }
        public TEvent Body { get; }
    }
}