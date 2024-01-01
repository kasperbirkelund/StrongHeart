using Cake.Core;
using Cake.Frosting;

namespace StrongHeart.Build;

public class StrongHeartBuildContext : FrostingContext
{
    public string? BuildConfiguration { get; set; }
    //public DotNetCoreMSBuildSettings MSBuildSettings { get; set; }

    public StrongHeartBuildContext(ICakeContext context)
        : base(context)
    {
    }
}