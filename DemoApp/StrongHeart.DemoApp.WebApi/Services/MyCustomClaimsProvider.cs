using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace StrongHeart.DemoApp.WebApi.Services
{
    public class MyCustomClaimsProvider : IClaimsProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyCustomClaimsProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ICollection<Claim> ExtractClaims()
        {
            if (_httpContextAccessor.HttpContext?.User != null)
            {
                return _httpContextAccessor.HttpContext.User.Claims.ToArray();
            }
            return Array.Empty<Claim>();
        }
    }
}