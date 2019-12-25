using System;
using System.Linq;

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
    }
}
