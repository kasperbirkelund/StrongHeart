using Cake.Common.Tools.DotNet;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks
{
    [IsDependentOn(typeof(BuildStrongHeartDemoApp))]
    public class UnitTestsDemoApp : FrostingTask<StrongHeartBuildContext>
    {
        public override void Run(StrongHeartBuildContext context)
        {
            context.DotNetTest("./DemoApp/", context.GetDotNetCoreTestSettings());
        }
    }
}