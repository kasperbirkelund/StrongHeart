using System;
using FluentAssertions;
using StrongHeart.Core.Correlation;
using Xunit;

namespace StrongHeart.Core.Test.Correlation
{
    public class CorrelationIdProviderTest
    {
        [Fact]
        public void UninitializedProviderShouldBeEmpty()
        {
            CorrelationIdProvider sut = new();
            sut.CorrelationId.Should().BeEmpty();
        }

        [Fact]
        public void ProviderCanOnlyBeInitializedOnce()
        {
            Guid g = Guid.NewGuid();
            CorrelationIdProvider sut = new();
            sut.Initialize(g);
            sut.Invoking(x => x.Initialize(g)).Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void InitializedProviderReturnsSameGuid()
        {
            Guid g = Guid.NewGuid();
            CorrelationIdProvider sut = new();
            sut.Initialize(g);
            sut.CorrelationId.Should().Be(g);
            sut.CorrelationId.Should().Be(g);
            sut.CorrelationId.Should().Be(g);
        }
    }
}
