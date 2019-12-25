using System;
using System.Collections.Generic;
using System.Diagnostics;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Helpers
{
    [DebuggerStepThrough]
    public class TestAdminCaller : ICaller
    {
        public Guid Id { get; } = new Guid("392519e8-2e2a-44ca-9757-693472c1b4b9");

        public IReadOnlyList<IRole> Roles { get; } = new List<IRole>()
        {
            AdminRole.Instance
        }.AsReadOnly();

        //public IUser? CallOnBehalfOf { get; } = null;
    }
}   