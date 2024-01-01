using System;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.Retry;

namespace StrongHeart.Features.Test.Decorators.Retry.Features.Queries.TestQuery;

public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IRetryable
{
    public Messenger Messenger { get; }

    public TestQueryFeature(Messenger messenger)
    {
        Messenger = messenger;
    }

    private int _attempt = 0;
    public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
    {
        Messenger.MethodIsExecuting();
        _attempt++;

        if (_attempt == 1)
        {
            throw new ArgumentNullException(nameof(request));
        }

        return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse(new PersonDto("PersonA"))));
    }

    public bool WhenExceptionIsThrownShouldIRetry(Exception exception, int currentAttempt)
    {
        return exception is ArgumentNullException;
    }
}