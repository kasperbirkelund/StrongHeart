using Cake.Frosting;

namespace StrongHeart.Build.Tasks
{
    [Dependency(typeof(UnitTests))]
    [Dependency(typeof(UnitTestsDemoApp))]
    public class CiBuild : FrostingTask
    {
    }
}