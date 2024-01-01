using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Decorators.ExceptionLogging;

namespace StrongHeart.Features.Test.Helpers;

public class ExceptionLoggerSpy : IExceptionLogger
{
    public IList<Exception> Exceptions = new List<Exception>();

    public Func<Exception, Task> Handler => Handle;

    private Task Handle(Exception ex)
    {
        Exceptions.Add(ex);
        return Task.CompletedTask;
    }
}