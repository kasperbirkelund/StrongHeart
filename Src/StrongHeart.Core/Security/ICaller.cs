using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace StrongHeart.Core.Security
{
    public interface ICaller
    {
        Guid Id { get; }
        IReadOnlyList<Claim> Claims { get; }
    }
}