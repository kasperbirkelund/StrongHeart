using System;
using System.Linq;
using StrongHeart.Features;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules
{
    public class CommandFeaturesRequestAndResponseMatch : IRule<Type>
    {
        public string CorrectiveAction => "Ensure that the command feature name is consistent with the request and dto name. Correct naming is CreateEmployeeFeature, CreateEmployeeRequest and CreateEmployeeDto.";
        public bool DoFailIfNoItemsToVerify => true;

        public bool DoVerifyItem(Type type)
        {
            return
                !type.IsAbstract && //this might be a CommandFeatureBase-class
                type.DoesImplementInterface(typeof(ICommandFeature<,>)) &&
                !type.DoesImplementInterface(typeof(ICommandDecorator<,>));
        }

        public bool IsValid(Type type, Action<string> output)
        {
            Type featureInterface = type.GetInterfaces().SingleOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandFeature<,>));
            if (featureInterface == null)
            {
                return false;
            }

            string featureNameRaw = type.Name.Replace("Feature", string.Empty);
            string featureRequestRaw = featureInterface.GetGenericArguments()[0].Name.Replace("Request", string.Empty);
            string featureDtoRaw = featureInterface.GetGenericArguments()[1].Name.Replace("Dto", string.Empty);

            return featureDtoRaw == featureNameRaw && featureRequestRaw == featureNameRaw;
        }
    }

}
