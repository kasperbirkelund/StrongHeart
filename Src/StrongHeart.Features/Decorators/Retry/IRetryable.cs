using System;

namespace StrongHeart.Features.Decorators.Retry
{
    public interface IRetryable
    {
        bool ShouldTryAgain(Exception exception, int currentAttempt);
    }
}