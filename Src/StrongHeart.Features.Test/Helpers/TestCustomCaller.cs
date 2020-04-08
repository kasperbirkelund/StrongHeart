using System;
using System.Collections.Generic;
using System.Diagnostics;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Helpers
{
    [DebuggerStepThrough]
    public class TestCustomCaller : ICaller
    {
        private readonly IRole[] _roles;

        public TestCustomCaller(params IRole[] roles)
        {
            _roles = roles;
        }
        public Guid Id { get; } = new Guid("f6b7c41c-2587-46c1-8746-cd9148587216");
        public IReadOnlyList<IRole> Roles => new List<IRole>(_roles).AsReadOnly();
        //public IUser? CallOnBehalfOf { get; } = null;
    }
}