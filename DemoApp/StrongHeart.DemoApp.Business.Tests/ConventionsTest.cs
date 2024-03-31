using System;
using System.Collections.Generic;
using System.Reflection;
using StrongHeart.DemoApp.Business.Features.Commands;
using StrongHeart.TestTools.ComponentAnalysis;
using Xunit;
using Xunit.Abstractions;

namespace StrongHeart.DemoApp.Business.Tests;

public class ConventionsTest(ITestOutputHelper helper) : ConventionsTestBase(helper.WriteLine)
{
    //all tests (Facts) are inherited from the base class so any future
    //additional tests in the StrongHeart base class will automatically be executed  

    protected override IEnumerable<Assembly> GetFeatureAssemblies()
    {
        yield return typeof(CommandFeatureBase<,>).Assembly;
    }


    //If you for one or another reason don't want a rule to be enforced - just skip it like this:
    //[Fact(Skip = "Rule is not enforced")]
    //public override void SingleItemResponseShouldBeImmutable()
    //{
    //}

    public static IEnumerable<object[]> TestMethods => GetTestMethods();

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