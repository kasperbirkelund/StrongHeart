using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using StrongHeart.Core.Security;

namespace StrongHeart.Features.Test.Helpers
{
    [DebuggerStepThrough]
    public class TestCustomCaller : ICaller
    {
        private readonly Claim[] _claims;

        public TestCustomCaller(params Claim[] claims)
        {
            _claims = claims;
        }
        public IReadOnlyList<Claim> Claims => new List<Claim>(_claims).AsReadOnly();
    }
}