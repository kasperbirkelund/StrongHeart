using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StrongHeart.Features.Decorators.TimeAlert
{
    public abstract class TimeAlertDecoratorBase : DecoratorBase
    {
        private readonly ILogger<TimeAlertDecoratorBase> _logger;

        protected TimeAlertDecoratorBase(ILogger<TimeAlertDecoratorBase> logger)
        {
            _logger = logger;
        }

        protected override async Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var response = await func(request);
                sw.Stop();
                return response;
            }
            finally
            {
                var max = GetMaxAllowedExecutionTime();
                if (sw.Elapsed > max)
                {
                    _logger.LogWarning("Max execution time exceeded {@Details}", new TimeExceededData(sw.Elapsed, request, max));
                }
            }
        }

        public abstract TimeSpan GetMaxAllowedExecutionTime();
    }
}