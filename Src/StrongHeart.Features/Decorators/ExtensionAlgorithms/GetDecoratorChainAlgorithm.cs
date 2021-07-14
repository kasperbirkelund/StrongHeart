using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StrongHeart.Features.Decorators.ExtensionAlgorithms
{
    internal class GetDecoratorChainAlgorithm
    {
        internal static IEnumerable<TDecorator> GetDecoratorChain<TFeature, TDecorator>(TFeature feature, Type featureType, Func<TDecorator, TFeature> getFeatureFromDecoratorFunc)
            where TDecorator : class
        {
            TFeature actualFeature = feature;
            while (true)
            {
                var innerDecorator = actualFeature as TDecorator;
                if (innerDecorator == null)
                {
                    break;
                }

                yield return innerDecorator;

                FieldInfo? innerFeature = GetInnerFeature(actualFeature!.GetType(), featureType);
                if (innerFeature == null)
                {
                    yield break;
                }

                actualFeature = getFeatureFromDecoratorFunc(innerDecorator);
            }
        }

        private static FieldInfo? GetInnerFeature(IReflect? type, Type featureType)
        {
            return type?.GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic)
                .SingleOrDefault(delegate (FieldInfo info)
                {
                    if (info.FieldType.IsGenericType)
                    {
                        return info.FieldType.GetGenericTypeDefinition() == featureType.GetGenericTypeDefinition();
                    }
                    return false;
                });
        }
    }
}