using System;
using StrongHeart.TestTools.ComponentAnalysis.Core;

namespace StrongHeart.TestTools.ComponentAnalysis.Test
{
    internal class DemoRuleWhichWillFail : IRule<Type>
    {
        public string CorrectiveAction => "Fix it";

        public bool DoVerifyItem(Type item)
        {
            return true;
        }

        public bool IsValid(Type item, Action<string> output)
        {
            output("Details goes here: " + item.IsClass);
            return false;
        }

        public bool DoFailIfNoItemsToVerify => true;
    }
}
