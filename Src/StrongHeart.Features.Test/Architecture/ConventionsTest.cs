using System;
using System.Collections.Generic;
using System.Reflection;
using StrongHeart.TestTools.ComponentAnalysis;
using Xunit;
using Xunit.Abstractions;

namespace StrongHeart.Features.Test.Architecture;

public class ConventionsTest(ITestOutputHelper helper) : ConventionsTestBase(helper.WriteLine)
{
    //all tests (Facts) are inherited from the base class so any future
    //additional tests in the StrongHeart base class will automatically be executed  

    protected override IEnumerable<Assembly> GetFeatureAssemblies()
    {
        yield return GetType().Assembly; //all features are in the same assembly as the test
    }

    public static IEnumerable<object[]> TestMethods => GetTestMethods();

    public override void EventHandlersMustDependOnACommandFeature()
    {
        // Skipped because the StrongHeart.Features.Test assembly does not contain any event handlers
    }

    [Theory]
    [MemberData(nameof(TestMethods))]
    public void RunConventionTest(string methodName)
    {
        var method = typeof(ConventionsTestBase).GetMethod(methodName);
        if (method == null)
        {
            throw new InvalidOperationException($"Test method '{methodName}' not found.");
        }
        method.Invoke(this, null);
    }
}