using System.Collections.Generic;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.Authorization
{
    public interface IAuthorizable
    {
        IEnumerable<IRole> GetRequiredRoles();
    }
}