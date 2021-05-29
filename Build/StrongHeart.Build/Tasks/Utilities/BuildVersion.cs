using Cake.Common.Tools.GitVersion;

namespace StrongHeart.Build.Tasks.Utilities
{
    public class BuildVersion
    {
        /// <summary>
        /// 0.9.0-frosting.2251
        /// </summary>
        public string FullSemVersion { get; }
        /// <summary>
        /// 0.9.0.2251
        /// </summary>
        public string AssemblySemVer { get; } 

        private BuildVersion(string assemblySemVer, string fullSemVersion)
        {
            AssemblySemVer = assemblySemVer;
            FullSemVersion = fullSemVersion;
        }

        public static BuildVersion Calculate(StrongHeartBuildContext context)
        {
            //if (!context.IsLocalBuild)
            {
                //context.GitVersion(new GitVersionSettings
                //{
                //    OutputType = GitVersionOutput.BuildServer
                //});
            }

            GitVersion assertedVersions = context.GitVersion(new GitVersionSettings
            {
                OutputType = GitVersionOutput.Json
            });
            return new BuildVersion(assertedVersions.AssemblySemVer, assertedVersions.FullSemVer);
        }
    }
}