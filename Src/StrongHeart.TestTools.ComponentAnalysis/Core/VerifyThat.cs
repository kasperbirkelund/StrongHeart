using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StrongHeart.TestTools.ComponentAnalysis.Core
{
    public static class VerifyThat
    {
        public static IEnumerable<Assembly> AllReferencedAssemblies(Assembly mainAssembly, Func<AssemblyName, bool> predicate)
        {
            IEnumerable<Assembly> assemblies = mainAssembly
                .GetReferencedAssemblies() //be aware that only USED referenced assemblies will be considered as 'referenced'
                .Where(predicate)
                .Select(Assembly.Load);

            return assemblies;
        }

        public static IEnumerable<Type> AllTypesFromAllReferencedAssemblies(Assembly mainAssembly, Func<AssemblyName, bool> predicate)
        {
            IEnumerable<Type> types =
                AllReferencedAssemblies(mainAssembly, predicate)
                .SelectMany(x => x.GetTypes())
                .Where(x => !IsAnonymousType(x))
                .Where(x => !x.FullName.Contains("Test"));

            return types;
        }

        private static bool IsAnonymousType(this Type type)
        {
            return type.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
        }

        public static IEnumerable<Type> AllTypesFromAssemblies(IEnumerable<Assembly> mainAssemblies)
        {
            IEnumerable<Type> types = mainAssemblies.SelectMany(x => x.GetTypes());
            return types;
        }

        public static IEnumerable<MethodInfo> AllMethodsFromTypes(this IEnumerable<Type> types)
        {
            return types.SelectMany(x => x.GetMethods());
        }
    }
}