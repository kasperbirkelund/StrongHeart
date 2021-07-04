using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Test.Decorators.RequestValidation.Features.Queries.TestQuery
{
    public class TestQueryFeature : IQueryFeature<TestQueryRequest, TestQueryResponse>, IRequestValidatable<TestQueryRequest>
    {
        public Task<Result<TestQueryResponse>> Execute(TestQueryRequest request)
        {
            return Task.FromResult(Result<TestQueryResponse>.Success(new TestQueryResponse("Hello")));
        }

        public Func<TestQueryRequest, ICollection<ValidationMessage>> ValidationFunc()
        {
            return request =>
            {
                List<ValidationMessage> messages = new(1);
                if (string.IsNullOrEmpty(request.ThisMustNotBeNull))
                {
                    messages.Add(nameof(request.ThisMustNotBeNull) + " must not be null or empty");
                }
                return messages;
            };
        }
    }
}