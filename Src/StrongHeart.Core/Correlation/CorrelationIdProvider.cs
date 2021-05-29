using System;

namespace StrongHeart.Core.Correlation
{
    public class CorrelationIdProvider : ICorrelationIdProvider
    {
        private bool _isInitialized = false;

        //do not check for _isInitialized in below get as it is ok that some logs are done with no correlation id (e.g. start of event consumer)
        public Guid CorrelationId { get; private set; }

        public void Initialize(Guid? newId = null)
        {
            if (_isInitialized)
            {
                throw new NotSupportedException($"{nameof(CorrelationIdProvider)} has already been initialized");
            }
            CorrelationId = newId ?? Guid.NewGuid();
            _isInitialized = true;
        }
    }
}