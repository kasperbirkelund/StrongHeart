using StrongHeart.Features.Test.Rules;
using StrongHeart.TestTools.ComponentAnalysis.Core;
using Xunit;

namespace StrongHeart.Features.Test
{
    public class RulesVerification
    {
        [Fact]
        public void A()
        {
            VerifyThat
                .AllTypesFromAssembly(GetType().Assembly)
                .DoesComplyToRule(new FeaturesCannotDependOnFeaturesRule());
        }
    }
}
