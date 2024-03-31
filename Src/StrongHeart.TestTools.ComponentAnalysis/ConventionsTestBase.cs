using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StrongHeart.TestTools.ComponentAnalysis;

public abstract partial class ConventionsTestBase(Action<string> output)
{
    protected abstract IEnumerable<Assembly> GetFeatureAssemblies();

    public static List<object[]> GetTestMethods()
    {
        var testMethods = typeof(ConventionsTestBase)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => !m.IsSpecialName) 
            .Select(m => new object[]
            {
                m.Name
            });

        return testMethods.ToList();
    }
}