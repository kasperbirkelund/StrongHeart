using Cake.Frosting;
using StrongHeart.Build.Tasks;

namespace StrongHeart.Build.EntryPoints
{
    //[Dependency(typeof(PushNuget))]
    [Dependency(typeof(CiBuild))]
    public class Default : FrostingTask
    {
    }
}