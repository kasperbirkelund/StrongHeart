using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StrongHeart.TestTools.ComponentAnalysis.Core
{
    public static class VerifyThat
    {
        //public static Component Component(Component component)
        //{
        //    return component;
        //}

        //public static IEnumerable<Component> Components(IEnumerable<Component> components)
        //{
        //    return components;
        //}

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

        public static IEnumerable<Type> AllTypesFromAllReferencedAssemblies(Assembly mainAssembly)
        {
            return AllTypesFromAllReferencedAssemblies(mainAssembly, _ => true);
        }

        private static bool IsAnonymousType(this Type type)
        {
            return type.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
        }

        public static IEnumerable<Type> AllTypesFromAssemblies(IEnumerable<Assembly> assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(x => x.GetTypes());
            return types;
        }

        public static IEnumerable<Type> AllTypesFromAssembly(Assembly assembly)
        {
            return AllTypesFromAssemblies(new[] {assembly});
        }

        public static IEnumerable<MethodInfo> AllMethodsFromTypes(this IEnumerable<Type> types)
        {
            return types.SelectMany(x => x.GetMethods());
        }

        public static IEnumerable<T> GetFromCustomSqlQuery<T>(Func<DbConnection> connectionFactory, string sql, Func<DbDataReader, T> readFunc)
        {
            using (var connection = connectionFactory())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    using DbDataReader reader = command.ExecuteReader();
                    List<T> list = new();
                    while (reader.Read())
                    {
                        list.Add(readFunc(reader));
                    }

                    return list;
                }
            }
        }
    }
}