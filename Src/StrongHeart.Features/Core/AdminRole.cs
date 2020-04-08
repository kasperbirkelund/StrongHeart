using System;
using StrongHeart.Core.Security;

namespace StrongHeart.Features.Core
{
    public class AdminRole : IRole
    {
        public static readonly AdminRole Instance = new AdminRole();

        private AdminRole()
        {
        }

        public string Id => new Guid("B519E366-B0AC-4D8D-A802-881D87D649A0").ToString();
        public string Name => "Administrator";
    }
}