using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace StrongHeart.Features.Decorators.ExceptionLogging
{
    public abstract class ExceptionLoggerDecoratorBase : DecoratorBase
    {
        private readonly ILogger _logger;

        protected ExceptionLoggerDecoratorBase(ILogger<ExceptionLoggerDecoratorBase> logger)
        {
            _logger = logger;
        }

        protected override async Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
        {
            try
            {
                return await func(request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception caught from StrongHeart while executing request {@request}", request);
                throw;
            }
        }
    }
}