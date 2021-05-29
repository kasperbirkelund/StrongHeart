using System;
using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Common.Tools.DotNetCore.Test;

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