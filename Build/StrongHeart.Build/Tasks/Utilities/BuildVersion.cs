using Cake.Common.Build;
using Cake.Common.Tools.GitVersion;
using Cake.Core;

namespace StrongHeart.Build.Tasks.Utilities
{
    public class BuildVersion
    {
        /// <summary>
        /// 0.9.0-frosting.2251
        /// </summary>
        public string FullSemVersion { get; }

        public string Sha { get; }

        /// <summary>
        /// 0.9.0.2251
        /// </summary>
        public string AssemblySemVer { get; } 

        private BuildVersion(string assemblySemVer, string fullSemVersion, string sha)
        {
            AssemblySemVer = assemblySemVer;
            FullSemVersion = fullSemVersion;
            Sha = sha;
        }

        public static BuildVersion Calculate(ICakeContext context)
        {
            if (context.BuildSystem().IsLocalBuild)
            {
                GitVersion assertedVersions = context.GitVersion(new GitVersionSettings
                {
                    //OutputType = GitVersionOutput.Json
                });
                return new BuildVersion(assertedVersions.AssemblySemVer, assertedVersions.FullSemVer,
                    assertedVersions.Sha);
            }
            else return new BuildVersion("0.9.0.0", "0.9.0.0", "no-sha-available");
        }

        public override string ToString()
        {
return @$"Sha: {Sha}
FullSemVersion: {FullSemVersion}
AssemblySemVer: {AssemblySemVer}";
        }
    }
}