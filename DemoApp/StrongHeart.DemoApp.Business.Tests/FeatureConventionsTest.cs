using System.Collections.Generic;
using System.Reflection;
using StrongHeart.DemoApp.Business.Features;
using StrongHeart.TestTools.Xunit;
using Xunit;

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


        //If you for one or another reason don't want a rule to be enforced - just skip it like this:
        //[Fact(Skip = "Rule is not enforced")]
        //public override void SingleItemResponseShouldBeImmutable()
        //{
        //}
    }
}