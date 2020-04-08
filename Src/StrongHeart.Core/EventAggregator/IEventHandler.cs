namespace StrongHeart.Core.EventAggregator
{
    public interface IEventHandler<in T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
