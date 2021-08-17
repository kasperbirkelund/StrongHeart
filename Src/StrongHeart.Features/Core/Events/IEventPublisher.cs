using System.Threading.Tasks;

namespace StrongHeart.Features.Core.Events
{
    public interface IEventPublisher
    {
        Task Publish<T>(T evt) where T : IEvent;
    }
}