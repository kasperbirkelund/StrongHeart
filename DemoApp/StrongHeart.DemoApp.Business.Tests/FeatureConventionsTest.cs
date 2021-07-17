using System.Collections.Generic;
using System.Reflection;
using StrongHeart.DemoApp.Business.Features;
using StrongHeart.TestTools.Xunit;

namespace StrongHeart.DemoApp.Business.Tests
{
    public class FeatureConventionsTest : FeatureConventionsTestBase
    {
        //all tests (Facts) are inherited from the base class so any future
        //additional tests in the StrongHeart base class will automatically be executed  
        protected override IEnumerable<Assembly> GetFeatureAssemblies()
        {
            yield return typeof(CommandFeatureBase<,>).Assembly;
        }
    }
}