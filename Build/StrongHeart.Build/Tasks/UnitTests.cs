using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks
{
    [Dependency(typeof(BuildStrongHeart))]
    public class UnitTests : FrostingTask<StrongHeartBuildContext>
    {
        public override void Run(StrongHeartBuildContext context)
        {
            context.DotNetCoreTest("./src/", context.GetDotNetCoreTestSettings());
        }
    }
}