using Cake.Common.Tools.DotNet;
using Cake.Frosting;
using StrongHeart.Build.Tasks.Utilities;

namespace StrongHeart.Build.Tasks;

[IsDependentOn(typeof(Clean))]
public class BuildStrongHeart : FrostingTask<StrongHeartBuildContext>
{
    public override void Run(StrongHeartBuildContext context)
    {
        context.DotNetBuild(@"./Src/StrongHeart.sln", context.GetDotNetCoreBuildSettings());

        //for some reason below tool is not build with the solution
        context.DotNetBuild(@"./Src/StrongHeart.FeatureTool/StrongHeart.FeatureTool.csproj", context.GetDotNetCoreBuildSettings());
    }
}