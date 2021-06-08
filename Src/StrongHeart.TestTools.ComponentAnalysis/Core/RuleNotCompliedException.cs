﻿using System;

namespace StrongHeart.TestTools.ComponentAnalysis.Core
{
    public class RuleNotCompliedException<T> : Exception
    {
        public RuleNotCompliedException(VerificationResult<T> result) 
            : base(result.Message + Environment.NewLine + result)
        {
        }
    }
}
