using System.Collections.Generic;
using System.Security.Claims;

namespace StrongHeart.Features.Decorators.Authorization
{
    public interface IAuthorizable
    {
        IEnumerable<Claim> GetRequiredClaims();
    }
}