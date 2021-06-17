using System;
using System.Threading.Tasks;

namespace StrongHeart.Features.Decorators.Retry
{
    public abstract class RetryDecoratorBase : DecoratorBase
    {
        protected override Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
        {
            IRetryable config = GetConfig();
            int attempt = 0;
            do
            {
                attempt++;
                try
                {
                    return func(request);
                }
                catch (Exception e)
                {
                    bool shouldTryAgain = config.WhenExceptionIsThrownShouldIRetry(e, attempt);
                    if (shouldTryAgain)
                    {
                        continue;
                    }
                    throw;
                }
            } while (true);
        }

        protected abstract IRetryable GetConfig();
    }
}