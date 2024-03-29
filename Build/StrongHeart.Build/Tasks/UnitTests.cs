using Cake.Common.Tools.DotNet;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks;

[IsDependentOn(typeof(BuildStrongHeart))]
public class UnitTests : FrostingTask<StrongHeartBuildContext>
{
    public override void Run(StrongHeartBuildContext context)
    {
        context.DotNetTest("./src/", context.GetDotNetCoreTestSettings());
    }
}