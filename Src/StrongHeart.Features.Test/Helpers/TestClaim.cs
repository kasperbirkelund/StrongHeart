using System.Diagnostics;
using System.Security.Claims;

namespace StrongHeart.Features.Test.Helpers;

[DebuggerStepThrough]
public class TestClaim
{
    public static readonly Claim Instance = new Claim(ClaimTypes.Role, "testClaim");

    private TestClaim()
    {
    }
}