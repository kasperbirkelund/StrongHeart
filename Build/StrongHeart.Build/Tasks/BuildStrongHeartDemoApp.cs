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
            context.DotNetCoreBuild(@".\StrongHeart.DempApp.Migrations.Runner\StrongHeart.DempApp.Migrations.Runner.csproj", context.GetDotNetCoreBuildSettings());
            context.DotNetCoreBuild(@".\DemoApp\StrongHeart.DemoApp.WebApi\StrongHeart.DemoApp.WebApi.csproj", context.GetDotNetCoreBuildSettings());
            context.DotNetCoreBuild(@".\DemoApp\StrongHeart.DempApp.EfCoreConsole\StrongHeart.DempApp.EfCoreConsole.csproj", context.GetDotNetCoreBuildSettings());
        }
    }
}