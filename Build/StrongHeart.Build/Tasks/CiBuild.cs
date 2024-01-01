using Cake.Frosting;

namespace StrongHeart.Build.Tasks;

[IsDependentOn(typeof(UnitTests))]
[IsDependentOn(typeof(UnitTestsDemoApp))]
public class CiBuild : FrostingTask
{
}