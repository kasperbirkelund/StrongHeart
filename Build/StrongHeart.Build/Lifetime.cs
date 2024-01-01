using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;

namespace StrongHeart.Build;

public class Lifetime : FrostingLifetime<StrongHeartBuildContext>
{
    public override void Setup(StrongHeartBuildContext context)
    {
        context.BuildConfiguration = context.Argument<string>("configuration", "Release");
        context.Information("Configuration: {0}", context.BuildConfiguration);
    }

    public override void Teardown(StrongHeartBuildContext context, ITeardownContext info)
    {
        //NO OP
    }
}