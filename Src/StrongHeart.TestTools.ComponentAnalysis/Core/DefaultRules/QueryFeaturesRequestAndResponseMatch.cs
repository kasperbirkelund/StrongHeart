using System;
using System.Linq;
using StrongHeart.Features;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules
{
    public class QueryFeaturesRequestAndResponseMatch : IRule<Type>
    {
        public string CorrectiveAction => "Ensure that the feature name is consistent with the request and response name";
        public bool DoFailIfNoItemsToVerify => true;

        public bool DoVerifyItem(Type type)
        {
            return
                type.DoesImplementInterface(typeof(IQueryFeature<,>)) &&
                !type.DoesImplementInterface(typeof(IQueryDecorator<,>));
        }

        public bool IsValid(Type type, Action<string> output)
        {
            Type featureInterface = type.GetInterfaces().SingleOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQueryFeature<,>));
            if (featureInterface == null)
            {
                return false;
            }

            string featureNameRaw = type.Name.Replace("Feature", string.Empty);
            string featureRequestRaw = featureInterface.GetGenericArguments()[0].Name.Replace("Request", string.Empty);
            string featureResponseRaw = featureInterface.GetGenericArguments()[1].Name.Replace("Response", string.Empty);

            return featureResponseRaw == featureNameRaw && featureRequestRaw == featureNameRaw;
        }
    }

}
