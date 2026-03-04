using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNet.NuGet.Push;
using Cake.Common.Tools.DotNet.Pack;
using Cake.Common.Tools.DotNet.Test;
using Cake.Core.IO.Arguments;
using Cake.Frosting;
using System;

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
        if (!isBuildServer)
        {
            return new NugetSourceInfo(null, @"C:\development\NugetFeed");
        }
        string? apiKey = Environment.GetEnvironmentVariable("NUGET_API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("NUGET_API_KEY environment variable must be set when pushing from CI.");
        }
        return new NugetSourceInfo(apiKey, "https://api.nuget.org/v3/index.json");
    }

    extension(StrongHeartBuildContext context)
    {
        public DotNetPackSettings GetDotNetPackSettings()
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
                builder.Append(new TextArgument($"/p:Version={version.AssemblySemVer}"));
                return builder;
            }
        };
    }

    public DotNetTestSettings GetDotNetTestSettings()
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

    public DotNetBuildSettings GetDotNetBuildSettings(Action<DotNetBuildSettings>? overwrite = null)
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