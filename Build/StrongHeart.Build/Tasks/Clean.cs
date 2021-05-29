using System;
using Cake.Common.Build;
using Cake.Common.IO;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Curl;
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
                context.GetDirectories("./src/**/obj");

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

    public class Tmp : FrostingTask<StrongHeartBuildContext>
    {
        public override void Run(StrongHeartBuildContext context)
        {
            context.Log.Verbosity = Verbosity.Diagnostic;
            //context.DupFinder();
            //context.DotCoverAnalyse();
            //context.ReportGenerator();
            //context.GitVersion()
            //context.Kudu().Sync(null, null, new KuduSyncSettings()

            string file = @".\install.ps1";
            string host = @"ftps://xxx.azurewebsites.windows.net/site/wwwroot/";
            //string host = @"ftps://waws-prod-am2-071.ftp.azurewebsites.windows.net/site/wwwroot/App_Data/jobs/continuous";
            string user2 = @"codeimprover-worker-test\$codeimprover-worker-test";
            string pass2 = @"xxx";

            //IKuduClient kuduClient = context.KuduClient(new KuduClientSettings("", "", ""));

            //kuduClient.ZipDeployDirectory(output);
            //context.ChocolateyInstall("sqllocaldb");

            //context.DotNetCoreTest("./src/", context.GetDotNetCoreTestSettings("Category=IntegrationTest"));

            Works(context, file, host, user2, pass2);
        }

        private static void Works(StrongHeartBuildContext context, string file, string host, string user2,
            string pass2)
        {
            context.CurlUploadFile(file, new Uri(host),
                new CurlSettings
                {
                    ProgressBar = true,
                    Username = user2,
                    Password = pass2,
                    RequestCommand = "POST",
                    MaxTime = TimeSpan.FromSeconds(20),
                    Fail = true,
                    Verbose = true
                });
        }
    }
}