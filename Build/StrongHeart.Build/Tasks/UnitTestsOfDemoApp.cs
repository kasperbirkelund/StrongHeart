using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks
{
    [Dependency(typeof(BuildStrongHeartDemoApp))]
    public class UnitTestsDemoApp : FrostingTask<StrongHeartBuildContext>
    {
        public override void Run(StrongHeartBuildContext context)
        {
            context.DotNetCoreTest("./DemoApp/", context.GetDotNetCoreTestSettings());
        }
    }
}