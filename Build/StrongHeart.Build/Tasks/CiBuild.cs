using Cake.Frosting;

namespace StrongHeart.Build.Tasks
{
    [IsDependentOn(typeof(UnitTests))]
    public class CiBuild : FrostingTask
    {
    }
}