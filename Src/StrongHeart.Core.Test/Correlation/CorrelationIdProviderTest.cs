using System;
using FluentAssertions;
using StrongHeart.Core.Correlation;
using Xunit;

namespace StrongHeart.Core.Test.Correlation;

public class CorrelationIdProviderTest
{
    [Fact]
    public void UninitializedProviderShouldBeEmpty()
    {
        CorrelationIdProvider sut = new(true);
        sut.CorrelationId.Should().BeEmpty();
    }

    [Fact]
    public void GetCorrelationIdWithEmptyGuidIsNotAllowed()
    {
        CorrelationIdProvider sut = new(false);
        sut.Invoking(x => x.CorrelationId).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void InitializeWithEmptyGuidIsNotAllowed()
    {
        CorrelationIdProvider sut = new(false);
        sut.Invoking(x => x.Initialize(Guid.Empty)).Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ProviderCanOnlyBeInitializedOnce(bool allowEmptyGuid)
    {
        Guid g = Guid.NewGuid();
        CorrelationIdProvider sut = new(allowEmptyGuid);
        sut.Initialize(g);
        sut.Invoking(x => x.Initialize(g)).Should().Throw<NotSupportedException>();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void InitializedProviderReturnsSameGuid(bool allowEmptyGuid)
    {
        Guid g = Guid.NewGuid();
        CorrelationIdProvider sut = new(allowEmptyGuid);
        sut.Initialize(g);
        sut.CorrelationId.Should().Be(g);
        sut.CorrelationId.Should().Be(g);
        sut.CorrelationId.Should().Be(g);
    }
}