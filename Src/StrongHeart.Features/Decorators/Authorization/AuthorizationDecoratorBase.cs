using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Authorization
{
    //[DebuggerStepThrough]
    public abstract class AuthorizationDecoratorBase : DecoratorBase
    {
        protected override Task<TResponse> Invoke<TRequest, TResponse>(Func<TRequest, Task<TResponse>> func,
            TRequest request)
        {
            return func(request);
        }

        protected string GetExceptionMessage(ICollection<IRole> requiredRoles)
        {
            string rolesAsString = string.Join(", ", requiredRoles.Select(x => $"'{x.Name}'"));
            string message = "You are not authorized to execute this feature. ";
            if (requiredRoles.Count == 1)
            {
                message += $"{rolesAsString} role is required";
            }
            else
            {
                message += $"One of these roles are required: {rolesAsString}";
            }
            return message;
        }

        protected bool IsAllowed(IEnumerable<IRole> userRoles, IEnumerable<IRole> requiredRoles)
        {
            bool isAllowed = (userRoles.Join(requiredRoles.Union(new[] { AdminRole.Instance }), u => u.Id, req => req.Id, (u, req) => 1)).Any();
            return isAllowed;
        }

        public abstract IEnumerable<IRole> GetRequiredRoles();
    }
}