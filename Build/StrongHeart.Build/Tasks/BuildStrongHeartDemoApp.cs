using Cake.Common.Tools.DotNet;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks;

//[Dependency(typeof(Clean))]
public class BuildStrongHeartDemoApp : FrostingTask<StrongHeartBuildContext>
{
    public override void Run(StrongHeartBuildContext context)
    {
        context.DotNetBuild(@".\StrongHeart.DempApp.Migrations.Runner\StrongHeart.DempApp.Migrations.Runner.csproj", context.GetDotNetCoreBuildSettings());
        context.DotNetBuild(@".\DemoApp\StrongHeart.DemoApp.WebApi\StrongHeart.DemoApp.WebApi.csproj", context.GetDotNetCoreBuildSettings());
        context.DotNetBuild(@".\DemoApp\StrongHeart.DempApp.EfCoreConsole\StrongHeart.DempApp.EfCoreConsole.csproj", context.GetDotNetCoreBuildSettings());
    }
}