using System;
using System.Threading.Tasks;

namespace StrongHeart.Features.Decorators.ExceptionLogging
{
    public abstract class ExceptionLoggerDecoratorBase : DecoratorBase
    {
        private readonly IExceptionLogger _logger;

        protected ExceptionLoggerDecoratorBase(IExceptionLogger logger)
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
                await _logger.Handler(e);
                throw;
            }
        }
    }
}