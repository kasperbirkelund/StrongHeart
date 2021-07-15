using StrongHeart.DemoApp.Business.Features.Queries.GetCars;
using StrongHeart.TestTools.ComponentAnalysis.Core;
using StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;
using Xunit;

namespace StrongHeart.DemoApp.Business.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            VerifyThat
                .AllTypesFromAssembly(typeof(GetCarsFeature).Assembly)
                .DoesComplyToRule(new QueryFeaturesRequestAndResponseMatch());
        }
    }
}