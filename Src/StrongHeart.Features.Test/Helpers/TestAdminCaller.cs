using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Helpers
{
    [DebuggerStepThrough]
    public class TestAdminCaller : ICaller
    { 
        public IReadOnlyList<Claim> Claims { get; } = new List<Claim>()
        {
            AdminClaim.Instance
        }.AsReadOnly();

        //public IUser? CallOnBehalfOf { get; } = null;
    }
}   