using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StrongHeart.TestTools.ComponentAnalysis.Core.ReferenceChecker;

namespace StrongHeart.TestTools.ComponentAnalysis.Test;

public static class Components
{
    public static IEnumerable<Component> GetAll()
    {
        var methods = typeof(Components).GetMethods(BindingFlags.Public | BindingFlags.Static);
        foreach (MethodInfo method in methods.Where(x => x.ReturnType == typeof(Component)))
        {
            var component = method.Invoke(null, null) as Component;
            yield return component;
        }
    }
}