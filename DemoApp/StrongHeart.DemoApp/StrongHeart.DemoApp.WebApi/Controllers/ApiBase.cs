using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using StrongHeart.Core.Security;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    public abstract class ApiBase : StrongHeartApiBase
    {
        private readonly IClaimsProvider _claimsProvider;

        protected ApiBase(IClaimsProvider claimsProvider)
        {
            _claimsProvider = claimsProvider;
        }

        protected override ICaller GetCaller()
        {
            //Read certificate, token, http context, whatever and extract claims
            return new WebApiCaller(_claimsProvider.ExtractClaims());
        }
    }

    public interface IClaimsProvider
    {
        ICollection<Claim> ExtractClaims();
    }

    public class MyCustomClaimsProvider : IClaimsProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyCustomClaimsProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ICollection<Claim> ExtractClaims()
        {
            return new Claim[0];
        }
    }
}