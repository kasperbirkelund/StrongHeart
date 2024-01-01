using System;
using System.Linq;
using StrongHeart.Features;

namespace StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;

public class EventHandlersMustDependOnACommandFeatureRule : IRule<Type>
{
    public string CorrectiveAction => "EventHandlers must depend on a command feature";
    public bool DoVerifyItem(Type item)
    {
        return item.IsEventHandlerInterface();
    }

    public bool IsValid(Type item, Action<string> output)
    {
        bool isValid = item.GetConstructors().SelectMany(x => x.GetParameters())
            .Any(x => x.ParameterType.IsCommandFeatureInterface());

        return isValid;
    }
}