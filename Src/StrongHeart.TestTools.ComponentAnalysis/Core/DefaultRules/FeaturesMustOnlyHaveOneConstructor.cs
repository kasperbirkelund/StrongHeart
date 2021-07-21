using System;
using StrongHeart.Features;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules
{
    public class FeaturesMustOnlyHaveOneConstructor : IRule<Type>
    {
        public string CorrectiveAction => "Ensure that a feature has exactly one public constructor";

        public bool DoVerifyItem(Type item)
        {
            return item.IsFeature();
        }

        public bool IsValid(Type item, Action<string> output)
        {
            return item.GetConstructors().Length <= 1;
        }
    }
}