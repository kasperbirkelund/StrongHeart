using System.Collections.Generic;
using System.Reflection;
using StrongHeart.TestTools.Xunit;
using Xunit.Abstractions;

namespace StrongHeart.Features.Test.Architecture;

public class FeatureConventionsTest : FeatureConventionsTestBase
{
    //all tests (Facts) are inherited from the base class so any future
    //additional tests in the StrongHeart base class will automatically be executed  

    public FeatureConventionsTest(ITestOutputHelper helper) : base(helper)
    {
    }

    protected override IEnumerable<Assembly> GetFeatureAssemblies()
    {
        yield return GetType().Assembly;
    }
}