using System;

namespace StrongHeart.Features.Decorators.Retry
{
    public interface IRetryable
    {
        bool WhenExceptionIsThrownShouldIRetry(Exception exception, int currentAttempt);
    }
}