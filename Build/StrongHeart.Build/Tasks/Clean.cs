using Cake.Common.Build;
using Cake.Common.IO;
using Cake.Core.IO;
using Cake.Frosting;

namespace StrongHeart.Build.Tasks
{
    public class Clean : FrostingTask<StrongHeartBuildContext>
    {
        public override void Run(StrongHeartBuildContext context)
        {
            DirectoryPathCollection directories =
                context.GetDirectories("./TestResult") +
                context.GetDirectories("./src/**/bin") +
                context.GetDirectories("./src/**/obj") +
                context.GetDirectories("./DemoApp/**/bin") +
                context.GetDirectories("./DemoApp/**/obj");

            foreach (DirectoryPath directory in directories)
            {
                context.CleanDirectory(directory);
            }
        }

        public override bool ShouldRun(StrongHeartBuildContext context)
        {
            //on the build server we always start with an empty instance/container
            return context.BuildSystem().IsLocalBuild;
        }
    }
}