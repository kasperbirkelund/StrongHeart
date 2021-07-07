using System;

namespace StrongHeart.Core.Correlation
{
    public class CorrelationIdProvider : ICorrelationIdProvider
    {
        private readonly bool _allowEmptyGuid;
        private Guid _correlationId = Guid.Empty;

        public CorrelationIdProvider(bool allowEmptyGuid)
        {
            _allowEmptyGuid = allowEmptyGuid;
        }
        private bool _isInitialized = false;

        public Guid CorrelationId
        {
            get
            {
                if (!_allowEmptyGuid && _correlationId == Guid.Empty)
                {
                    throw new InvalidOperationException("Correlation id has not been initialized. Please call Initialize()");
                }
                return _correlationId;
            }
            private set
            {
                if (!_allowEmptyGuid && value == Guid.Empty)
                {
                    throw new InvalidOperationException("Correlation id cannot be set to Guid.Empty.");
                }
                _correlationId = value;
            }
        }


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