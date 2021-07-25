using System;
using FluentAssertions;
using StrongHeart.TestTools.ComponentAnalysis.Core;
using Xunit;

namespace StrongHeart.TestTools.ComponentAnalysis.Test
{
    public class VerifyThatTest
    {
        [Fact]
        public void DemoRuleWhichWillSelectNoTypes_TypesWithFailures_ThrowsException()
        {
            var result = VerifyThat
                .AllTypesFromAssembly(GetType().Assembly)
                .DoesComplyToRule(new DemoRuleWhichWillSelectNoTypes(), false);

            result.IsPassed.Should().BeFalse();
        }

        [Fact]
        public void DemoRuleWhichWillFail_TypesWithFailures_ThrowsException()
        {
            Action action = () =>
            {
                VerifyThat
                    .AllTypesFromAssembly(GetType().Assembly)
                    .DoesComplyToRule(new DemoRuleWhichWillFail());
            };
            action.Should().Throw<RuleNotCompliedException>();
        }

        [Fact]
        public void DemoRuleWhichWillFail_TypesWithFailures_ReturnsCorrectly()
        {
            VerificationResult<Type> result =
                VerifyThat
                    .AllTypesFromAssembly(GetType().Assembly)
                    .DoesComplyToRule(new DemoRuleWhichWillFail(), false);

            //below part is not intended to be used in regular test scenarios. Just for demonstration.
            result.IsPassed.Should().BeFalse();
            result.AllVerifiedItems.Count.Should().BeGreaterThan(0);
            result.ItemsWithError.Count.Should().BeGreaterThan(0);
            result.Message.Should().NotBe("All verified items comply to rule");
            result.Output.Should().NotBeNullOrWhiteSpace();
        }
    }
}
