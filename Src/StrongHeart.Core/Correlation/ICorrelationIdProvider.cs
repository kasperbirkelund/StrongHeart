using System;

namespace StrongHeart.Core.Correlation
{
    public interface ICorrelationIdProvider
    {
        Guid CorrelationId { get; }
        void Initialize(Guid? newId = null);
    }
}
