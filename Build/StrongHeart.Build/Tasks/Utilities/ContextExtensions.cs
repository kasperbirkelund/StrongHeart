using System;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNet.NuGet.Push;
using Cake.Common.Tools.DotNet.Pack;
using Cake.Common.Tools.DotNet.Test;
using Cake.Frosting;

namespace StrongHeart.Build.Tasks.Utilities;

public record NugetSourceInfo(string? ApiKey, string Source);
public static class ContextExtensions
{
    private static readonly DotNetVerbosity? CakeVerbosity = DotNetVerbosity.Quiet;

    public static DotNetNuGetPushSettings GetDotNetNuGetPushSettings(this FrostingContext context)
    {
        NugetSourceInfo sourceInfo = GetNugetSourceInfo(context.GitHubActions().IsRunningOnGitHubActions);
            
        return new DotNetNuGetPushSettings
        {
            Source = sourceInfo.Source,
            ApiKey = sourceInfo.ApiKey,
            SkipDuplicate = false,
            ForceEnglishOutput = true
        };
    }

    private static NugetSourceInfo GetNugetSourceInfo(bool isBuildServer)
    {
        //dotnet nuget push "**/*.nupkg" - s C:\development\NugetFeed
        if (!isBuildServer)
        {
            return new NugetSourceInfo(null, @"C:\development\NugetFeed");
        }
        throw new NotImplementedException("");
        //return new NugetSourceInfo(null, "https://api.nuget.org/v3/index.json");
    }

    public static DotNetPackSettings GetDotNetPackSettings(this StrongHeartBuildContext context)
    {
        BuildVersion version = BuildVersion.Calculate(context);
        return new()
        {
            Configuration = context.BuildConfiguration,
            NoLogo = true,
            NoBuild = true,
            Verbosity = CakeVerbosity,
            ArgumentCustomization = builder =>
            {
                builder.Append($"/p:Version={version.AssemblySemVer}");
                return builder;
            }
        };
    }

    public static DotNetTestSettings GetDotNetTestSettings(this StrongHeartBuildContext context)
    {
        return new()
        {
            Configuration = context.BuildConfiguration,
            NoBuild = true,
            NoRestore = true,
            NoLogo = true,
            Verbosity = CakeVerbosity,
        };
    }

    public static DotNetBuildSettings GetDotNetBuildSettings(this StrongHeartBuildContext context, Action<DotNetBuildSettings>? overwrite = null)
    {
        DotNetBuildSettings settings = new()
        {
            //Framework = Framework,
            Configuration = context.BuildConfiguration,
            NoRestore = false,
            NoLogo = true,
            Verbosity = CakeVerbosity,
            MSBuildSettings = GetDotNetCoreMsBuildSettings(context)
        };
        overwrite?.Invoke(settings);
        return settings;
    }

    private static DotNetMSBuildSettings GetDotNetCoreMsBuildSettings(FrostingContext context)
    {
        BuildVersion version = BuildVersion.Calculate(context);
        context.Information(version.ToString());
        DotNetMSBuildSettings settings = new DotNetMSBuildSettings()
            .WithProperty("WarningLevel", "0")
            .WithProperty("Version", version.AssemblySemVer)
            .WithProperty("AssemblyVersion", version.AssemblySemVer)
            .WithProperty("FileVersion", version.AssemblySemVer)
            .WithProperty("SourceRevisionId", version.Sha);

        return settings;
    }
}