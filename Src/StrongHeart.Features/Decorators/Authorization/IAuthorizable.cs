using System.Collections.Generic;
using StrongHeart.Core.Security;

namespace StrongHeart.Features.Decorators.Authorization
{
    public interface IAuthorizable
    {
        IEnumerable<IRole> GetRequiredRoles();
    }
}