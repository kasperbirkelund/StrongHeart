using System.IO;
using Cake.Common.Tools.DotNet;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks
{
    [IsDependentOn(typeof(PackNuget))]
    public class PushNuget : FrostingTask<StrongHeartBuildContext>
    {
        public override void Run(StrongHeartBuildContext context)
        {
            foreach (string nugetPackage in Directory.EnumerateFiles(@".\Src\", "*.nupkg", SearchOption.AllDirectories))
            {
                context.DotNetNuGetPush(nugetPackage, context.GetDotNetCoreNuGetPushSettings());
            }
        }
    }
}