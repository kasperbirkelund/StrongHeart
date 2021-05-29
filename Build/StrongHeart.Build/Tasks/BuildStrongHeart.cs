using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks
{
    [Dependency(typeof(Clean))]
    public class BuildStrongHeart : FrostingTask<StrongHeartBuildContext>
    {
        public override void Run(StrongHeartBuildContext context)
        {
            context.DotNetCoreBuild(@".\Src\StrongHeart.sln", context.GetDotNetCoreBuildSettings());
        }
    }
}