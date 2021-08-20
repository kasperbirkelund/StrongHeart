using System;
using System.Data;
using FluentAssertions;
using Moq;
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

        [Fact]
        public void A()
        {
            int i = 0;
            Mock<IDataReader> readerMock = new Mock<IDataReader>();
            readerMock.Setup(x => x.Read()).Returns(() => i++ < 5);

            Mock<IDbCommand> comMock = new Mock<IDbCommand>();
            comMock.Setup(x => x.ExecuteReader()).Returns(readerMock.Object);

            Mock<IDbConnection> conMock = new Mock<IDbConnection>();
            conMock.Setup(x => x.CreateCommand()).Returns(comMock.Object);

            var result =
                VerifyThat
                    .GetFromCustomSqlQuery(() => conMock.Object, "SELECT * FROM Nothing", reader => new A(4))
                    .DoesComplyToRule(new B());
        }
    }
    public class B : IRule<A>
    {
        public string CorrectiveAction => "";

        public bool DoVerifyItem(A item)
        {
            return true;
        }

        public bool IsValid(A item, Action<string> output)
        {
            return true;
        }
    }
    public record A(int Id);
}
