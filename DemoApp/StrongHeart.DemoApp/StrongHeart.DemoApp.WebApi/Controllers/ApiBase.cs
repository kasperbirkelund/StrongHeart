using StrongHeart.Core.Security;
using StrongHeart.DemoApp.WebApi.Services;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    public abstract class ApiBase : StrongHeartApiBase
    {
        private readonly IClaimsProvider _claimsProvider;

        protected ApiBase(IClaimsProvider claimsProvider)
        {
            _claimsProvider = claimsProvider;
        }

        protected ICaller GetCaller()
        {
            //Read certificate, token, http context, whatever and extract claims
            return new WebApiCaller(_claimsProvider.ExtractClaims());
        }
    }
}