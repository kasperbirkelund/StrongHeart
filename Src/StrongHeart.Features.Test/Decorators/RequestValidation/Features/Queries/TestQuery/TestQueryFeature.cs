using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Queries.TestQuery;

public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IRequestValidatable<TestQueryRequest>
{
    public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
    {
        return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse("Hello")));
    }

    public Func<TestQueryRequest, IEnumerable<ValidationMessage>> ValidationFunc() => ValidateRequest;

    private IEnumerable<ValidationMessage> ValidateRequest(TestQueryRequest request)
    {
        if (string.IsNullOrEmpty(request.ThisMustNotBeNull))
        {
            yield return nameof(request.ThisMustNotBeNull) + " must not be null or empty";
        }
    }
}