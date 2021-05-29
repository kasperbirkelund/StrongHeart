using System;
using Cake.Common;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core.Diagnostics;

namespace StrongHeart.Build.Tasks.Utilities
{
    public static class ContextExtensions
    {
        private const DotNetCoreVerbosity Verbosity = DotNetCoreVerbosity.Minimal;

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

        private static DotNetCoreMSBuildSettings GetDotNetCoreMsBuildSettings(StrongHeartBuildContext context)
        {
            context.Log.Verbosity = Cake.Core.Diagnostics.Verbosity.Diagnostic;

            BuildVersion version = BuildVersion.Calculate(context);
            DotNetCoreMSBuildSettings settings = new DotNetCoreMSBuildSettings()
                    .WithProperty("WarningLevel", "0")
                    .WithProperty("Version", version.AssemblySemVer)
                    .WithProperty("AssemblyVersion", version.AssemblySemVer)
                    .WithProperty("FileVersion", version.AssemblySemVer)
                ;
            string sha = context.EnvironmentVariable("Sha");
            if (string.IsNullOrWhiteSpace(sha))
            {
                context.Log.Warning("Environment variable 'Sha' is not found. Skipping...");
            }
            else
            {
                settings.WithProperty("SourceRevisionId", sha);
            }

            return settings;
        }
    }
}