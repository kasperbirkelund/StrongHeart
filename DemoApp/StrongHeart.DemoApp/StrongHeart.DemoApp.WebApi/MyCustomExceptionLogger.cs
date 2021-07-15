using System;
using System.Threading.Tasks;
using StrongHeart.Features.Decorators.ExceptionLogging;

namespace StrongHeart.DemoApp.WebApi
{
    public class MyCustomExceptionLogger : IExceptionLogger
    {
        public Func<Exception, Task> Handler => exception => Task.CompletedTask;
    }
}