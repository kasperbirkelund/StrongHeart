using System;
using StrongHeart.TestTools.ComponentAnalysis.Core;

namespace StrongHeart.TestTools.ComponentAnalysis.Test;

internal class DemoRuleWhichWillSelectNoTypes : IRule<Type>
{
    public string CorrectiveAction => "Fix it";
    public bool DoVerifyItem(Type item)
    {
        return false;
    }
    public bool IsValid(Type item, Action<string> output) => throw new NotSupportedException();
}