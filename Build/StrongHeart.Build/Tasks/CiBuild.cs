using Cake.Frosting;

namespace StrongHeart.Build.Tasks
{
    [Dependency(typeof(UnitTests))]
    public class CiBuild : FrostingTask
    {
    }
}