using System;
using System.Threading.Tasks;
using StrongHeart.Features.Decorators;

namespace StrongHeart.Features.Test.SampleDecorator.SimpleLog;

public abstract class SimpleLogDecoratorBase : DecoratorBase
{
    private readonly ISimpleLog _simpleLog;

    protected SimpleLogDecoratorBase(ISimpleLog simpleLog)
    {
        _simpleLog = simpleLog;
    }

    protected override async Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
    {
        _simpleLog.Log("Test message before");
        var result = await func(request);
        _simpleLog.Log("Test message after");
        return result;
    }
}