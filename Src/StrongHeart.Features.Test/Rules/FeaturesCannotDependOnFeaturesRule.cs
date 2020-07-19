using System;
using System.Linq;
using System.Reflection;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;
using StrongHeart.TestTools.ComponentAnalysis.Core;

namespace StrongHeart.Features.Test.Rules
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
            ParameterInfo[] failedItems = item.GetConstructors().Single().GetParameters().Where(x => IsFeature(x.ParameterType)).ToArray();

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
            //IsAssignableToGenericType(item, typeof(IEventHandlerFeature<>))
        }

        private bool IsFeatureDecorator(Type item)
        {
            return
                IsAssignableToGenericType(item, typeof(ICommandDecorator<,>)) ||
                IsAssignableToGenericType(item, typeof(IQueryDecorator<,>));
                //IsAssignableToGenericType(item, typeof(IEventHandlerDecorator<>));
        }

        private static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}
