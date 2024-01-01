using StrongHeart.Core.Security;

namespace StrongHeart.DemoApp.WebApi.Controllers;

public class WebApiCallerProvider : ICallerProvider
{
    private readonly IClaimsProvider _claimsProvider;

    public WebApiCallerProvider(IClaimsProvider claimsProvider)
    {
        _claimsProvider = claimsProvider;
    }

    public ICaller GetCurrentCaller()
    {
        //Read certificate, token, http context, whatever and extract claims
        return new WebApiCaller(_claimsProvider.ExtractClaims());
    }
}