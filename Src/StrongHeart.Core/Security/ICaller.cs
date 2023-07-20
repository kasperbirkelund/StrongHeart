using System.Collections.Generic;
using System.Security.Claims;

namespace StrongHeart.Core.Security
{
    public interface ICaller
    {
        IReadOnlyList<Claim> Claims { get; }
    }
}