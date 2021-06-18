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

        public static bool IsFeature(this Type type)
        {
            return type.IsCommandFeature() || type.IsQueryFeature();
        }

        public static bool IsCommandFeature(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(ICommandFeature<,>);
        }

        public static bool IsQueryFeature(this Type type)
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