using System;
using System.Collections.Generic;
using System.Security.Claims;
using StrongHeart.Core.Security;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    public class WebApiCaller : ICaller
    {
        public Guid Id => Guid.Empty;

        public IReadOnlyList<Claim> Claims => new List<Claim>();
    }
}