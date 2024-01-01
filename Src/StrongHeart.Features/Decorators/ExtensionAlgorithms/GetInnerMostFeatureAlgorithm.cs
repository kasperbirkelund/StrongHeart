using System;

namespace StrongHeart.Features.Decorators.ExtensionAlgorithms;

internal class GetInnerMostFeatureAlgorithm
{
    internal static TFeature GetInnerMostFeature<TFeature, TDecorator>(TDecorator decorator, Func<TDecorator, TFeature> getFeatureFromDecoratorFunc)
        where TDecorator : class
    {
        TFeature inner = getFeatureFromDecoratorFunc(decorator);
        while (true)
        {
            var innerDecorator = inner as TDecorator;
            if (innerDecorator == null)
            {
                break;
            }
            inner = getFeatureFromDecoratorFunc(innerDecorator);
        }
        return inner;
    }
}