using System;
using System.Linq;
using StrongHeart.Core.Security;
using StrongHeart.Features;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;

public class EventHandlersMustDependOnACommandFeatureRule : IRule<Type>
{
    public string CorrectiveAction => "EventHandlers must depend on a command feature and a ICallerProvider";
    public bool DoVerifyItem(Type item)
    {
        return item.IsEventHandlerInterface();
    }

    public bool IsValid(Type item, Action<string> output)
    {
        var constructors = item.GetConstructors();
        if (constructors.Length != 1)
        {
            output("There must be exactly one constructor");
            return false;
        }

        var parameters = constructors.Single().GetParameters();
        if (parameters.Length != 2)
        {
            output("There must be exactly two parameters");
            return false;
        }
        if (!parameters.First().ParameterType.IsCommandFeatureInterface())
        {
            output("The first parameter must be of type ICommandFeature");
            return false;
        }
        if (parameters.Last().ParameterType != typeof(ICallerProvider))
        {
            output("The last parameter must be of type ICallerProvider");
            return false;
        }
        return true;
    }
}