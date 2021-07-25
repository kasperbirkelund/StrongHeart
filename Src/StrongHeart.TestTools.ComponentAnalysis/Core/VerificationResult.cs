using System;
using System.Collections.Generic;
using System.Linq;

namespace StrongHeart.TestTools.ComponentAnalysis.Core
{
    public class VerificationResult<T>
    {
        public IList<T> AllVerifiedItems { get; }
        public IList<T> ItemsWithError { get; }

        public bool IsPassed => ItemsWithError.Count == 0 && AllVerifiedItems.Count > 0;
        public string Message { get; }
        public string? Output { get; }

        private VerificationResult(IList<T> allVerifiedItems, IList<T> itemsWithError, string message, string? output)
        {
            AllVerifiedItems = allVerifiedItems;
            ItemsWithError = itemsWithError;
            Message = message;
            Output = output;
        }

        internal static VerificationResult<T> NoItemsToVerify(bool throwOnException)
        {
            VerificationResult<T> result = new(new T[0], new T[0], "No items found for verification. Check your selection.", null);
            if (throwOnException)
            {
                throw new RuleNotCompliedException(result.ToString());
            }
            return result;
        }

        internal static VerificationResult<T> NoErrors(IList<T> allVerifiedItems)
        {
            return new(allVerifiedItems, new T[0], "All verified items comply to rule", null);
        }

        internal static VerificationResult<T> ErrorsFound(IList<T> allVerifiedItems, IList<T> itemsWithError, string correctiveAction, bool throwOnException, string output)
        {
            VerificationResult<T> result = new(allVerifiedItems, itemsWithError, correctiveAction, output);
            if (throwOnException)
            {
                throw new RuleNotCompliedException(result.ToString());
            }
            return result;
        }
        
        public override string ToString()
        {
            string output = string.IsNullOrWhiteSpace(Output) ? string.Empty : $"Details: {Output}";
            return
$@"
Message: {Message}
Types verified: {AllVerifiedItems.Count}
Types with error {ItemsWithError.Count}:
{string.Join(Environment.NewLine, ItemsWithError.Select(x => "-" + x))}
{output}";
        }
    }
}