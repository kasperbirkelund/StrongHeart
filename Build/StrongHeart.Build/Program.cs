using System;
using Cake.Frosting;

namespace StrongHeart.Build
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .UseContext<StrongHeartBuildContext>()
                .UseLifetime<Lifetime>()
                .UseWorkingDirectory(@"..\..")
                .SetToolPath(@".\.cakeTools")
                .InstallTool(new Uri("nuget:?package=GitVersion.CommandLine&version=5.6.9"))
                .Run(args);
        }
    }
}
