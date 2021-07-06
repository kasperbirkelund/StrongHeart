using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StrongHeart.Features.Decorators.TimeAlert
{
    public abstract class TimeAlertDecoratorBase : DecoratorBase
    {
        private readonly ITimeAlertExceededLogger _logger;

        protected TimeAlertDecoratorBase(ITimeAlertExceededLogger logger)
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
                if (sw.Elapsed > GetMaxAllowedExecutionTime())
                {
                    await _logger.LogTimeExceeded(new TimeExceededData(sw.Elapsed, request));
                }
            }
        }

        public abstract TimeSpan GetMaxAllowedExecutionTime();
    }
}