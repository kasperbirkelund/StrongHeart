using System.Security.Claims;

namespace StrongHeart.Features.Core
{
    public static class AdminClaim
    {
        public static readonly Claim Instance = new Claim(ClaimTypes.Role, "strongheart-admin");
    }
}