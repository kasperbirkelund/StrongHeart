using StrongHeart.Features.Core.Events;
using StrongHeart.Features.Decorators.ExtensionAlgorithms;

namespace StrongHeart.Features;

public static class Extensions
{
    extension(Type type)
    {
        public bool DoesImplementInterface(Type @interface)
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

    public bool IsFeatureInterface()
    {
        return type.IsCommandFeatureInterface() || type.IsQueryFeatureInterface();
    }

    /// <summary>
    /// Returns false is the type is a decorator or an abstract class
    /// </summary>
    public bool IsCommandFeatureInterface()
    {
        return IsInterface(type, typeof(ICommandFeature<,>));
    }

    /// <summary>
    /// Returns false is the type is a decorator or an abstract class
    /// </summary>
    public bool IsQueryFeatureInterface()
    {
        return IsInterface(type, typeof(IQueryFeature<,>));
    }

    public bool IsEventHandlerInterface()
    {
        return type is
        {
            IsClass: true,
            IsAbstract: false
        } && type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<,>));
    }

    public bool IsFeatureClass()
    {
        return IsFeatureClassAlgorithm.IsFeatureClass(type);
    }

    public bool IsFeatureDecorator()
    {
        return IsFeatureClassAlgorithm.IsFeatureDecorator(type);
    }
    }

    private static bool IsInterface(Type type, Type interfaceType)
    {
        if (!type.IsGenericType || !type.IsInterface)
        {
            return false;
        }
        Type typeDefinition = type.GetGenericTypeDefinition();
        return typeDefinition == interfaceType;
    }
}