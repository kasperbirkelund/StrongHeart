using System;
using System.Threading.Tasks;

namespace StrongHeart.Features.Decorators.ExceptionLogging;

public interface IExceptionLogger
{
    Func<Exception, Task> Handler { get; }
}