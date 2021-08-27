using System.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks
{
    [Dependency(typeof(PackNuget))]
    public class PushNuget : FrostingTask<StrongHeartBuildContext>
    {
        public override void Run(StrongHeartBuildContext context)
        {
            foreach (string nugetPackage in Directory.EnumerateFiles(@".\Src\", "*.nupkg", SearchOption.AllDirectories))
            {
                context.DotNetCoreNuGetPush(nugetPackage, context.GetDotNetCoreNuGetPushSettings());
            }
        }
    }
}