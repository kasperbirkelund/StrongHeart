using Cake.Frosting;
using StrongHeart.Build.Tasks;

namespace StrongHeart.Build.EntryPoints;

//[Dependency(typeof(PushNuget))]
[IsDependentOn(typeof(CiBuild))]
[IsDependentOn(typeof(CalculateMetrics))]
public class Default : FrostingTask
{
}