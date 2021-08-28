using Cake.Frosting;
using StrongHeart.Build.Tasks;

namespace StrongHeart.Build.EntryPoints
{
    [Dependency(typeof(PushNuget))]
    public class Default : FrostingTask
    {
    }
}