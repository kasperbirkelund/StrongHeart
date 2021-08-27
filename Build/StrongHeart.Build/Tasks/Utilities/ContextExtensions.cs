using System;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Common.Tools.DotNetCore.NuGet.Push;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core;
using Cake.Frosting;

namespace StrongHeart.Build.Tasks.Utilities
{
    public record NugetSourceInfo(string? ApiKey, string Source);
    public static class ContextExtensions
    {
        private const DotNetCoreVerbosity Verbosity = DotNetCoreVerbosity.Quiet;

        public static DotNetCoreNuGetPushSettings GetDotNetCoreNuGetPushSettings(this FrostingContext context)
        {
            NugetSourceInfo sourceInfo = GetNugetSourceInfo(context.GitHubActions().IsRunningOnGitHubActions);
            
            return new DotNetCoreNuGetPushSettings
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

        public static DotNetCorePackSettings GetDotNetCorePackSettings(this StrongHeartBuildContext context)
        {
            BuildVersion version = BuildVersion.Calculate(context);
            return new()
            {
                Configuration = context.BuildConfiguration,
                NoLogo = true,
                NoBuild = true,
                Verbosity = Verbosity,
                ArgumentCustomization = builder =>
                {
                    builder.Append($"/p:Version={version.AssemblySemVer}");
                    return builder;
                }
            };
        }

        public static DotNetCoreTestSettings GetDotNetCoreTestSettings(this StrongHeartBuildContext context)
        {
            return new()
            {
                Configuration = context.BuildConfiguration,
                NoBuild = true,
                NoRestore = true,
                NoLogo = true,
                Verbosity = Verbosity,
            };
        }

        public static DotNetCoreBuildSettings GetDotNetCoreBuildSettings(this StrongHeartBuildContext context, Action<DotNetCoreBuildSettings>? overwrite = null)
        {
            DotNetCoreBuildSettings settings = new()
            {
                //Framework = Framework,
                Configuration = context.BuildConfiguration,
                NoRestore = false,
                NoLogo = true,
                Verbosity = Verbosity,
                MSBuildSettings = GetDotNetCoreMsBuildSettings(context)
            };
            overwrite?.Invoke(settings);
            return settings;
        }

        private static DotNetCoreMSBuildSettings GetDotNetCoreMsBuildSettings(FrostingContext context)
        {
            BuildVersion version = BuildVersion.Calculate(context);
            context.Information(version.ToString());
            DotNetCoreMSBuildSettings settings = new DotNetCoreMSBuildSettings()
                    .WithProperty("WarningLevel", "0")
                    .WithProperty("Version", version.AssemblySemVer)
                    .WithProperty("AssemblyVersion", version.AssemblySemVer)
                    .WithProperty("FileVersion", version.AssemblySemVer)
                    .WithProperty("SourceRevisionId", version.Sha);

            return settings;
        }
    }
}