using Cake.Frosting;
using StrongHeart.Build.Tasks;

namespace StrongHeart.Build.EntryPoints
{
    [IsDependentOn(typeof(CiBuild))]
    [IsDependentOn(typeof(CalculateMetrics))]
    //[IsDependentOn(typeof(PushNuget))]
    public class Default : FrostingTask
    {
    }
}