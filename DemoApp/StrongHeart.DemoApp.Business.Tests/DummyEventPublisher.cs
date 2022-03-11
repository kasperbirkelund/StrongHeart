using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core.Events;

namespace StrongHeart.DemoApp.Business.Tests;

internal class DummyEventPublisher : IEventPublisher
{
    public Task Publish<T>(T evt) where T : class, IEvent
    {
        throw new NotImplementedException();
    }
}