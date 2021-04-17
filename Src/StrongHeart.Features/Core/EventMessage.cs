//using StrongHeart.Core.Security;

//namespace StrongHeart.Features.Core
//{
//    public class EventMessage<T> : IRequest where T : class, IEvent
//    {
//        public EventMessage(ICaller caller, T body, EventMetadata metadata)
//        {
//            Body = body;
//            Metadata = metadata;
//            Caller = caller;
//        }

//        public EventMetadata Metadata { get; }
//        public T Body { get; }
//        public ICaller Caller { get; }
//    }
//}