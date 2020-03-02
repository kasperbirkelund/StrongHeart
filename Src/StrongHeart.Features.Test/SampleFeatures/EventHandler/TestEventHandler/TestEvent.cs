using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.SampleFeatures.EventHandler.TestEventHandler
{
    public class TestEvent : IEvent
    {
        public TestEvent(string newValue)
        {
            NewValue = newValue;
        }

        public string NewValue { get; }
    }
}