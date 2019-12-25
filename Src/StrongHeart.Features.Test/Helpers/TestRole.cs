using System;
using System.Diagnostics;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Test.Helpers
{
    [DebuggerStepThrough]
    public class TestRole : IRole
    {
        public static readonly TestRole Instance = new TestRole();

        private TestRole()
        {
        }

        public string Id => new Guid("5343dde1-48b5-4a69-926c-fac4b5e539a9").ToString();
        public string Name => "Test";
    }
}