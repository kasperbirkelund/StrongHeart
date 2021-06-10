using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Authorization
{
    public abstract class AuthorizationDecoratorBase : DecoratorBase
    {
        protected override Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func, TRequest request)
        {
            return func(request);
        }

        protected string GetExceptionMessage(ICollection<Claim> requiredClaims)
        {
            string claimsAsString = string.Join(", ", requiredClaims.Select(x => $"'{x.Value}'"));
            string message = "You are not authorized to execute this feature. ";
            if (requiredClaims.Count == 1)
            {
                message += $"{claimsAsString} claim is required";
            }
            else
            {
                message += $"One of these claims are required: {claimsAsString}";
            }
            return message;
        }

        protected bool IsAllowed(IReadOnlyList<Claim> userClaims, IEnumerable<Claim> requiredClaims)
        {
            var requiredWithAdmin = requiredClaims.Union(new[] {AdminClaim.Instance});
            var q = from uc in userClaims
                join r in requiredWithAdmin on new {uc.Type, uc.Value} equals new {r.Type, r.Value}
                select uc;
            
            return q.Any();
        }

        public abstract IEnumerable<Claim> GetRequiredClaims();
    }
}