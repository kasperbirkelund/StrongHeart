using System.Collections.Generic;
using System.Security.Claims;

namespace StrongHeart.DemoApp.WebApi.Services
{
    public interface IClaimsProvider
    {
        ICollection<Claim> ExtractClaims();
    }
}