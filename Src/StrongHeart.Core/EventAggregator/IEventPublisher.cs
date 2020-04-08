namespace StrongHeart.Core.EventAggregator
{
    public interface IEventPublisher
    {
        void PublishEvent<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
        //Task PublishEventAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}