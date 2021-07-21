using System;
using System.Linq;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;

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

        /// <summary>
        /// Returns false is the type is a decorator or an abstract class
        /// </summary>
        public static bool IsDecorator(this Type type)
        {
            return
                type.DoesImplementInterface(typeof(ICommandDecorator<,>)) ||
                type.DoesImplementInterface(typeof(IQueryDecorator<,>));
        }

        /// <summary>
        /// Returns false is the type is a decorator or an abstract class
        /// </summary>
        public static bool IsCommandFeature(this Type type)
        {
            if (!type.IsGenericType || type.IsAbstract || type.IsDecorator())
            {
                return false;
            }

            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(ICommandFeature<,>);
        }

        /// <summary>
        /// Returns false is the type is a decorator or an abstract class
        /// </summary>
        public static bool IsQueryFeature(this Type type)
        {
            if (!type.IsGenericType || type.IsAbstract || type.IsDecorator())
            {
                return false;
            }
            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(IQueryFeature<,>);
        }
    }
}