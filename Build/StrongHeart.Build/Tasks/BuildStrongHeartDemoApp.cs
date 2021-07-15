using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks
{
    //[Dependency(typeof(Clean))]
    public class BuildStrongHeartDemoApp : FrostingTask<StrongHeartBuildContext>
    {
        public override void Run(StrongHeartBuildContext context)
        {
            context.DotNetCoreBuild(@".\DemoApp\StrongHeart.DemoApp\StrongHeart.DemoApp.sln", context.GetDotNetCoreBuildSettings());
        }
    }
}