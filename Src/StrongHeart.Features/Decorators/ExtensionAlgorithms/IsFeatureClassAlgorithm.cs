using System;
using System.Linq;
using StrongHeart.Features.Core;

namespace StrongHeart.Features.Decorators.ExtensionAlgorithms;

internal class IsFeatureClassAlgorithm
{
    internal static bool IsFeatureClass(Type item)
    {
        return
            !item.IsAbstract &&
            IsAssignableToGenericType(item, typeof(ICommandFeature<,>)) ||
            IsAssignableToGenericType(item, typeof(IQueryFeature<,>));
    }

    internal static bool IsFeatureDecorator(Type item)
    {
        return
            IsAssignableToGenericType(item, typeof(ICommandDecorator<,>)) ||
            IsAssignableToGenericType(item, typeof(IQueryDecorator<,>));
    }

    private static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        var interfaceTypes = givenType.GetInterfaces();

        if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType))
        {
            return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
        {
            return true;

        }

        Type? baseType = givenType.BaseType;
        if (baseType == null)
        {
            return false;
        }

        return IsAssignableToGenericType(baseType, genericType);
    }
}