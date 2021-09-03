using System;
using System.Linq;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.ExtensionAlgorithms;

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

        //public static bool DoesImplementInterface(this Type type, Type genericType)
        //{
        //    if (genericType.IsGenericType)
        //    {
        //        return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType);
        //    }

        //    if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
        //    {
        //        return true;
        //    }

        //    Type? baseType = type.BaseType;
        //    if (baseType == null)
        //    {
        //        return false;
        //    }

        //    return DoesImplementInterface(baseType, genericType);

        //    //return @interface.IsAssignableFrom(type);
        //}

        public static bool IsFeatureInterface(this Type type)
        {
            return type.IsCommandFeatureInterface() || type.IsQueryFeatureInterface();
        }

        ///// <summary>
        ///// Returns false is the type is a decorator or an abstract class
        ///// </summary>
        //public static bool IsDecorator(this Type type)
        //{
        //    return
        //        type.DoesImplementInterface(typeof(ICommandDecorator<,>)) ||
        //        type.DoesImplementInterface(typeof(IQueryDecorator<,>));
        //}

        /// <summary>
        /// Returns false is the type is a decorator or an abstract class
        /// </summary>
        public static bool IsCommandFeatureInterface(this Type type)
        {
            return IsFeatureInterface(type, typeof(ICommandFeature<,>));
        }

        /// <summary>
        /// Returns false is the type is a decorator or an abstract class
        /// </summary>
        public static bool IsQueryFeatureInterface(this Type type)
        {
            return IsFeatureInterface (type, typeof(IQueryFeature<,>));
        }

        public static bool IsFeatureClass(this Type type)
        {
            return IsFeatureClassAlgorithm.IsFeatureClass(type);
        }

        public static bool IsFeatureDecorator(this Type type)
        {
            return IsFeatureClassAlgorithm.IsFeatureDecorator(type);
        }

        private static bool IsFeatureInterface(Type type, Type interfaceType)
        {
            if (!type.IsGenericType || !type.IsInterface)
            {
                return false;
            }
            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == interfaceType;
        }
    }
}