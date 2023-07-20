using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StrongHeart.Features.Decorators.ExceptionLogging;

namespace StrongHeart.Features.Test.Helpers
{
    public class ExceptionLoggerSpy : ILogger<ExceptionLoggerDecoratorBase>
    {
        public IList<Exception> Exceptions = new List<Exception>();
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (exception is not null)
            {
                Exceptions.Add(exception);
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