using StrongHeart.TestTools.ComponentAnalysis.Core;
using StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;
using Xunit;

namespace StrongHeart.Features.Test
{
    public class RulesVerification
    {
        [Fact]
        public void FeaturesCannotDependOnFeatures()
        {
            VerifyThat
                .AllTypesFromAssembly(GetType().Assembly)
                .DoesComplyToRule(new FeaturesCannotDependOnFeaturesRule());
        }
    }
}
