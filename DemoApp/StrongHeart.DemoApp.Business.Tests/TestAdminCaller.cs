using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace StrongHeart.DemoApp.Business.Tests;

[DebuggerStepThrough]
public class TestAdminCaller : ICaller
{
    public Guid Id { get; } = new Guid("dd2219e8-2e2a-44ca-9722-a93472c124b9");

    public IReadOnlyList<Claim> Claims { get; } = new List<Claim>()
    {
        AdminClaim.Instance
    }.AsReadOnly();
}