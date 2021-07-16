using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using StrongHeart.Core.Security;

namespace StrongHeart.DemoApp.WebApi.Controllers
{
    public class WebApiCaller : ICaller
    {
        private readonly ICollection<Claim> _claims;

        public WebApiCaller(ICollection<Claim> claims)
        {
            _claims = claims;
        }

        public Guid Id => Guid.Empty; //TODO: insert a unique proper guid
        public IReadOnlyList<Claim> Claims => _claims.ToList();
    }
}