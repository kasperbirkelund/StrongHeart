using Cake.Frosting;
using StrongHeart.Build.Tasks;

namespace StrongHeart.Build.EntryPoints
{
    //[Dependency(typeof(PushNuget))]
    [Dependency(typeof(CiBuild))]
    [Dependency(typeof(CalculateMetrics))]
    public class Default : FrostingTask
    {
    }
}