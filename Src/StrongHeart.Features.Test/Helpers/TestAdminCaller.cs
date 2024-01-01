using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Helpers;

[DebuggerStepThrough]
public class TestAdminCaller : ICaller
{
    public Guid Id { get; } = new Guid("392519e8-2e2a-44ca-9757-693472c1b4b9");

    public IReadOnlyList<Claim> Claims { get; } = new List<Claim>()
    {
        AdminClaim.Instance
    }.AsReadOnly();

    //public IUser? CallOnBehalfOf { get; } = null;
}