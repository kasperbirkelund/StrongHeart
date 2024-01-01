using FluentAssertions;
using FluentAssertions.Execution;
using StrongHeart.Features.Core;
using StrongHeart.Features.Test.SampleFeatures.Queries.TestQuery;
using StrongHeart.Features.Test.SampleFeatures.Command.TestCommand;
using BaseClassQ = StrongHeart.Features.Test.SampleFeaturesWithBaseClass.Queries.TestQuery;
using BaseClassC = StrongHeart.Features.Test.SampleFeaturesWithBaseClass.Command.TestCommand;
using Xunit;

namespace StrongHeart.Features.Test;

public class ExtensionsTest
{
    [Fact]
    public void TestQueryFeature_NoBaseClass_IsFeature()
    {
        using (new AssertionScope())
        {
            typeof(TestQueryFeature).IsQueryFeatureInterface().Should().BeFalse();
            typeof(TestQueryFeature).IsCommandFeatureInterface().Should().BeFalse();
            typeof(TestQueryFeature).IsFeatureInterface().Should().BeFalse();
            typeof(TestQueryRequest).IsFeatureInterface().Should().BeFalse();

            typeof(IQueryFeature<TestQueryRequest, TestQueryResponse>).IsQueryFeatureInterface().Should().BeTrue();
            typeof(IQueryFeature<TestQueryRequest, TestQueryResponse>).IsCommandFeatureInterface().Should().BeFalse();
            typeof(IQueryFeature<TestQueryRequest, TestQueryResponse>).IsFeatureInterface().Should().BeTrue();
        }
    }

    [Fact]
    public void TestCommandFeature_NoBaseClass_IsFeature()
    {
        using (new AssertionScope())
        {
            typeof(TestCommandFeature).IsQueryFeatureInterface().Should().BeFalse();
            typeof(TestCommandFeature).IsCommandFeatureInterface().Should().BeFalse();
            typeof(TestCommandFeature).IsFeatureInterface().Should().BeFalse();
            typeof(TestCommandRequest).IsFeatureInterface().Should().BeFalse();

            typeof(ICommandFeature<TestCommandRequest, TestCommandDto>).IsQueryFeatureInterface().Should().BeFalse();
            typeof(ICommandFeature<TestCommandRequest, TestCommandDto>).IsCommandFeatureInterface().Should().BeTrue();
            typeof(ICommandFeature<TestCommandRequest, TestCommandDto>).IsFeatureInterface().Should().BeTrue();
        }
    }

    [Fact]
    public void TestQueryFeature_WithBaseClass_IsFeature()
    {
        using (new AssertionScope())
        {
            typeof(BaseClassQ.TestQueryFeature).IsQueryFeatureInterface().Should().BeFalse();
            typeof(BaseClassQ.TestQueryFeature).IsCommandFeatureInterface().Should().BeFalse();
            typeof(BaseClassQ.TestQueryFeature).IsFeatureInterface().Should().BeFalse();
        }
    }

    [Fact]
    public void TestCommandFeature_WithBaseClass_IsFeature()
    {
        using (new AssertionScope())
        {
            typeof(BaseClassC.TestCommandFeature).IsQueryFeatureInterface().Should().BeFalse();
            typeof(BaseClassC.TestCommandFeature).IsCommandFeatureInterface().Should().BeFalse();
            typeof(BaseClassC.TestCommandFeature).IsFeatureInterface().Should().BeFalse();
        }
    }
}