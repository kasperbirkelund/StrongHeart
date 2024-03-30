using System.Collections.Generic;
using System.Reflection;
using Xunit.Abstractions;

namespace StrongHeart.TestTools.Xunit;

public abstract partial class ConventionsTestBase(ITestOutputHelper helper)
{
    protected abstract IEnumerable<Assembly> GetFeatureAssemblies();
}