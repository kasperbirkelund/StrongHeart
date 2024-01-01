using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Retry;

namespace StrongHeart.Features.Test.Decorators.Retry.Features.Command.TestCommand;

public class TestCommandFeature : ICommandFeature<TestCommandRequest, TestCommandDto>, IRetryable
{
    public Messenger Messenger { get; }

    public TestCommandFeature(Messenger messenger)
    {
        Messenger = messenger;
    }

    private int _attempt = 0;
    public Task<Result> Execute(TestCommandRequest request)
    {
        Messenger.MethodIsExecuting();
        _attempt++;

        if (_attempt == 1)
        {
            throw new ArgumentNullException(nameof(request));
        }

        return Task.FromResult(Result.Success());
    }

    public bool WhenExceptionIsThrownShouldIRetry(Exception exception, int currentAttempt)
    {
        return exception is ArgumentNullException;
    }
}