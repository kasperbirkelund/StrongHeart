using System;
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
        public Guid Id { get; } = new Guid("f6b7c41c-2587-46c1-8746-cd9148587216");
        public IReadOnlyList<Claim> Claims => new List<Claim>(_claims).AsReadOnly();
    }
}