using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.IO.Arguments;
using Cake.Frosting;
using StrongHeart.Build.MetricsSchema;

namespace StrongHeart.Build.Tasks;

public class CalculateMetrics : FrostingTask<StrongHeartBuildContext>
{
    public override void Run(StrongHeartBuildContext context)
    {
        DirectoryPath workingDir = context.FileSystem.GetDirectory(new DirectoryPath("./.cakework")).Path;
        DirectoryPath roslynCloneDir = workingDir.Combine(new DirectoryPath("roslyn"));
        FilePath metricsExe = roslynCloneDir.CombineWithFilePath(@"artifacts\bin\Metrics\Release\net472\Metrics.exe");
        FilePath reportPath = new FilePath("./report.xml");

        context.EnsureDirectoryExists(workingDir);
        IFile? restoreFile = GetRestoreFile(roslynCloneDir, context);
        if (restoreFile is null)
        {
            PrepareMetricTool(context, workingDir, roslynCloneDir);
        }
        else
        {
            context.Information("Metric tool is already installed :-)");
        }
        context.StartProcess(metricsExe, @"/solution:.\StrongHeart.sln /out:report.xml");
        
        IEnumerable<MetricResult> metrics = GetMetrics(reportPath);
        const int maxAllowedCyclomaticComplexity = 10;

        List<MetricResult> res = metrics
            .Where(x=> x.CyclomaticComplexity > maxAllowedCyclomaticComplexity)
            .ToList();

        if (res.Any())
        {
            string message = string.Join(Environment.NewLine, res.OrderByDescending(x => x.CyclomaticComplexity));
            throw new CakeException(-1, "These types does not conform to CyclomaticComplexity rule of " + maxAllowedCyclomaticComplexity + message);
        }
        else
        {
            context.Information($"No metric exceeds the allowed threshold of {maxAllowedCyclomaticComplexity}");
        }
    }

    private IEnumerable<MetricResult> GetMetrics(FilePath reportPath)
    {
        var methods = XDocument
            .Load(reportPath.FullPath)
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
        
        XmlSerializer ser = new XmlSerializer(typeof(Method));
        var q = from m in methods
            let me = GetMethod(m, ser)
            let cy = int.Parse(me.Metrics!.Single(x => x.Name == "CyclomaticComplexity").Value!)
            select new MetricResult(me.Name!, cy);
        return q.ToImmutableArray();
    }

    private void PrepareMetricTool(StrongHeartBuildContext context, DirectoryPath workingdir, DirectoryPath roslynDir)
    {
        //Step 1
        RunProcessAndWait(context, "git", "clone --depth=1 https://github.com/dotnet/roslyn-analyzers.git roslyn",
            workingdir);

        //Step 2
        FilePath restoreCommand = GetRestoreFile(roslynDir, context)!.Path;
        RunProcessAndWait(context, restoreCommand.FullPath, null, null);

        //Step 3
        var metricsFolder = roslynDir.Combine(new DirectoryPath(@"src\Tools\Metrics"));
        context.DotNetBuild(metricsFolder + "/Metrics.csproj", new DotNetCoreBuildSettings()
        {
            ArgumentCustomization = delegate(ProcessArgumentBuilder builder)
            {
                builder.Append(new TextArgument("-m"));
                return builder;
            },
            Configuration = "Release",
            DiagnosticOutput = false,
            NoLogo = true
        });
    }

    private static void RunProcessAndWait(StrongHeartBuildContext context, string command, string? args, DirectoryPath? workingdir)
    {
        var settings = new ProcessSettings();
        if (args != null)
        {
            settings.Arguments = ProcessArgumentBuilder.FromString(args);
        }
        if (workingdir != null)
        {
            settings.WorkingDirectory = workingdir;
        }

        using (var p1 = context.StartAndReturnProcess(command, settings))
        {
            p1.WaitForExit();
        }
    }

    private Method GetMethod(XElement element, XmlSerializer ser)
    {
        using TextReader reader = new StringReader(element.ToString());
        return (ser.Deserialize(reader) as Method)!;
    }


    private IFile? GetRestoreFile(DirectoryPath roslynDir, StrongHeartBuildContext context)
    {
        try
        {
            return context.FileSystem.GetDirectory(roslynDir).GetFiles("Restore.cmd", SearchScope.Current)
                .FirstOrDefault();
        }
        catch (DirectoryNotFoundException)
        {
            return null;
        }
    }

    private record MetricResult(string Name, int CyclomaticComplexity);
}