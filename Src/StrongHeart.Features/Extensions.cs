using System;
using System.Linq;
using StrongHeart.Features.Core;

namespace StrongHeart.Features
{
    public static class Extensions
    {
        public static bool DoesImplementInterface(this Type type, Type @interface)
        {
            if (@interface.IsGenericType)
            {
                return type.GetInterfaces()
                    .Where(x => x.IsGenericType)
                    .Select(x => x.GetGenericTypeDefinition())
                    .Any(x => x == @interface);
            }

            return @interface.IsAssignableFrom(type);
        }

        internal static bool IsFeatureInterface(this Type type)
        {
            return IsCommand(type) || IsQuery(type);
        }

        internal static bool IsCommand(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(ICommandFeature<,>);
        }

        internal static bool IsQuery(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }
            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(IQueryFeature<,>);
        }
    }
}
