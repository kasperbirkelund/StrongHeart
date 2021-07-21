using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StrongHeart.Features;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules
{
    public class FeaturesCannotDependOnFeaturesRule : IRule<Type>
    {
        public string CorrectiveAction => "Features should not depend on other features. Refactor to share the logic.";
        public bool DoVerifyItem(Type item)
        {
            return item.IsFeature();
        }

        public bool IsValid(Type item, Action<string> output)
        {
            List<ParameterInfo> failedItems = new();
            foreach (var ctr in item.GetConstructors())
            {
                failedItems.AddRange(ctr.GetParameters().Where(x => x.ParameterType.IsFeature()));
            }

            if (failedItems.Any())
            {
                output(string.Join(" ,", failedItems.Select(x => x.Name)));
                return false;
            }
            return true;
        }
    }
}
