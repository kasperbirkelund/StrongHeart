using Cake.Frosting;
using StrongHeart.Build.Tasks;

namespace StrongHeart.Build.EntryPoints;

[IsDependentOn(typeof(PushNuget))]
//[IsDependentOn(typeof(CiBuild))]
public class Default : FrostingTask
{
}