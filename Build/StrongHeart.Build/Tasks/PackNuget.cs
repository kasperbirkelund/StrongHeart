using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks
{
    [Dependency(typeof(CiBuild))]
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
                "StrongHeart.TestTools.Xunit"
            };

            foreach (string project in projectsToNugetPack)
            {
                context.DotNetCorePack(@$".\Src\{project}\{project}.csproj", context.GetDotNetCorePackSettings());
            }
        }
    }
}
