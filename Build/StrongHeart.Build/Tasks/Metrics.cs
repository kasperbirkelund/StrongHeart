using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.IO.Arguments;
using Cake.Frosting;
using Cake.Git;
using StrongHeart.Build.Metrics;

namespace StrongHeart.Build.Tasks;

public class CalculateMetrics : FrostingTask<StrongHeartBuildContext>
{
    public override void Run(StrongHeartBuildContext context)
    {
        context.Log.Verbosity = Verbosity.Diagnostic;
        DirectoryPath workingdir = context.FileSystem.GetDirectory(new DirectoryPath("./.cakework")).Path;
        context.EnsureDirectoryExists(workingdir);

        DirectoryPath roslynDir = workingdir.Combine(new DirectoryPath("roslyn"));
        IFile? restoreFile = GetRestoreFile(roslynDir, context);

        if (restoreFile is null)
        {
            context.StartProcess("git", "clone --depth=1 https://github.com/dotnet/roslyn-analyzers.git");
            //context.GitClone("https://github.com/dotnet/roslyn-analyzers.git", roslynDir);

            FilePath restore = GetRestoreFile(roslynDir, context)!.Path;
            using (IProcess process = context.StartAndReturnProcess(restore))
            {
                process.WaitForExit();
                context.Information("Exit code: {0}", process.GetExitCode());
            }

            var metricsFolder = roslynDir.Combine(new DirectoryPath(@"src\Tools\Metrics"));
            context.DotNetCoreBuild(metricsFolder + "/Metrics.csproj", new DotNetCoreBuildSettings()
            {
                ArgumentCustomization = delegate (ProcessArgumentBuilder builder)
                {
                    builder.Append(new TextArgument("-m"));
                    return builder;
                },
                Configuration = "Release"
            });
        }
        FilePath metricsExe = roslynDir.CombineWithFilePath(@"artifacts\bin\Metrics\Release\net472\Metrics.exe");
        context.StartProcess(metricsExe, @"/solution:.\StrongHeart.sln /out:report.xml");

        FilePath report = new FilePath("./report.xml");

        var doc = XDocument.Load(report.FullPath);
        var methods = doc
            .Descendants("CodeMetricsReport")
            .Descendants("Targets")
            .Descendants("Target")
            .Descendants("Assembly")
            .Descendants("Namespaces")
            .Descendants("Namespace")
            .Descendants("Types")
            .Descendants("NamedType")
            .Descendants("Members")
            .Descendants("Method");

        int maxCy = 10;
        XmlSerializer ser = new XmlSerializer(typeof(Method));
        var q =
            from m in methods
            let me = GetMethod(m, ser)
            let cy = int.Parse(me.Metrics.Single(x => x.Name == "CyclomaticComplexity").Value)
            where cy > maxCy
            select new
            {
                cy,
                me.Name
            };

        var res = q.ToList();

        if (res.Any())
        {
            string message = string.Join(Environment.NewLine, res.OrderByDescending(x => x.cy));
            throw new CakeException(-1, "These types does not conform to CyclomaticComplexity rule of " + maxCy + message);
        }
    }

    private Method GetMethod(XElement element, XmlSerializer ser)
    {
        using TextReader reader = new StringReader(element.ToString());
        var m = ser.Deserialize(reader) as Method;
        return m;
    }


    private IFile? GetRestoreFile(DirectoryPath roslynDir, StrongHeartBuildContext context)
    {
        return context.FileSystem.GetDirectory(roslynDir).GetFiles("Restore.cmd", SearchScope.Current).FirstOrDefault();
    }
}