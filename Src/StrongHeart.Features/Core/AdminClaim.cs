using System.Security.Claims;

namespace StrongHeart.Features.Core
{
    public class AdminClaim
    {
        public static readonly Claim Instance = new Claim(ClaimTypes.Role, "admin");

        private AdminClaim()
        {
        }
    }
}