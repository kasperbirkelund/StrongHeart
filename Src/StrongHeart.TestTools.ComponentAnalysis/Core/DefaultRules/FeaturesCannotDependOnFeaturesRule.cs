using System;
using System.Linq;
using System.Reflection;
using StrongHeart.Features;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules
{
    public class FeaturesCannotDependOnFeaturesRule : IRule<Type>
    {
        public string CorrectiveAction => "Features should not depend on other features. Refactor to share the logic.";
        public bool DoVerifyItem(Type item)
        {
            return IsFeature(item) && !IsFeatureDecorator(item);
        }

        public bool IsValid(Type item, Action<string> output)
        {
            ParameterInfo[] failedItems = item.GetConstructors().Single().GetParameters().Where(x => x.ParameterType.IsFeature()).ToArray();

            if (!failedItems.Any()) 
                return true;

            output(string.Join(" ,", failedItems.Select(x => x.Name)));
            return false;
        }

        public bool DoFailIfNoItemsToVerify => true;

        private bool IsFeature(Type item)
        {
            return
                IsAssignableToGenericType(item, typeof(ICommandFeature<,>)) ||
                IsAssignableToGenericType(item, typeof(IQueryFeature<,>));
        }

        private bool IsFeatureDecorator(Type item)
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

            Type baseType = givenType.BaseType;
            if (baseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}
