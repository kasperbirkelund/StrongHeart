using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StrongHeart.Features.Decorators.TimeAlert;

namespace StrongHeart.Features.Test.Helpers
{
    public class TimeAlertExceededLoggerSpy : ILogger<TimeAlertDecoratorBase>
    {
        public readonly IList<string> Data = new List<string>();
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var msg = formatter(state, exception);
            if(msg.Contains("Max execution time exceeded"))
            {
                Data.Add(msg);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotSupportedException();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotSupportedException();
        }
    }
}