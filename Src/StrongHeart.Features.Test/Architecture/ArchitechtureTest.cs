using System;
using FluentAssertions;
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
            VerificationResult<Type> result = VerifyThat
                .AllTypesFromAssembly(GetType().Assembly)
                .DoesComplyToRule(new QueryFeaturesRequestAndResponseMatch());

            //below part is not intended to be used in regular test scenarios. Just for demonstration.
            result.IsPassed.Should().BeTrue();
            result.AllVerifiedItems.Count.Should().BeGreaterThan(0);
            result.ItemsWithError.Count.Should().Be(0);
            result.Message.Should().Be("All verified items comply to rule");
            result.Output.Should().BeNull();
        }

        [Fact]
        public void GivenAllCommandFeatures_WhenCheckNaming_ThenNoErrors()
        {
            VerifyThat
                .AllTypesFromAssembly(GetType().Assembly)
                .DoesComplyToRule(new CommandFeaturesRequestAndResponseMatch());
        }

        [Fact]
        public void ResponseDtoShouldBeMoreSpecific()
        {
            VerifyThat
                .AllTypesFromAssembly(GetType().Assembly)
                .DoesComplyToRule(new ResponseDtoShouldBeMoreSpecificRule());
        }

        [Fact]
        public void FeaturesCannotDependOnFeatures()
        {
            VerifyThat
                .AllTypesFromAssembly(GetType().Assembly)
                .DoesComplyToRule(new FeaturesCannotDependOnFeaturesRule());
        }
    }
}
