using StrongHeart.TestTools.ComponentAnalysis.Core;
using StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;
using Xunit;

namespace StrongHeart.Features.Test.Architecture
{
    public class ArchitectureTest
    {
        [Fact]
        public void GivenAllQueryFeatures_WhenCheckNaming_ThenNoErrors()
        {
            VerifyThat
                .AllTypesFromAssembly(GetType().Assembly)
                .DoesComplyToRule(new QueryFeaturesRequestAndResponseMatch());
        }

        [Fact]
        public void GivenAllCommandFeatures_WhenCheckNaming_ThenNoErrors()
        {
            VerifyThat
                .AllTypesFromAssembly(GetType().Assembly)
                .DoesComplyToRule(new CommandFeaturesRequestAndResponseMatch());
        }
    }
}
