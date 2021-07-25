using System;

namespace StrongHeart.TestTools.ComponentAnalysis.Core
{
    public class RuleNotCompliedException : Exception
    {
        //Consider take VerificationResult<T> as constructor argument
        public RuleNotCompliedException(string message) 
            : base(message)
        {
        }
    }
}
