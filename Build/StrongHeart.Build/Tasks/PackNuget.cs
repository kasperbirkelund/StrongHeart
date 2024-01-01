using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks;

[IsDependentOn(typeof(CiBuild))]
public class PackNuget : FrostingTask<StrongHeartBuildContext>
{
    public override void Run(StrongHeartBuildContext context)
    {
        context.DeleteFiles("**/*.nupkg");
        context.DeleteFiles("**/*.nuspec");

        string[] projectsToNugetPack =
        {
            "StrongHeart.Features",
            "StrongHeart.Features.AspNetCore",
            "StrongHeart.Features.Documentation",
            "StrongHeart.Core",
            "StrongHeart.EfCore",
            "StrongHeart.Migrations",
            "StrongHeart.TestTools.ComponentAnalysis",
            "StrongHeart.TestTools.Xunit",
            "StrongHeart.FeatureTool"
        };

        foreach (string project in projectsToNugetPack)
        {
            context.DotNetPack(@$".\Src\{project}\{project}.csproj", context.GetDotNetCorePackSettings());
        }
    }
}