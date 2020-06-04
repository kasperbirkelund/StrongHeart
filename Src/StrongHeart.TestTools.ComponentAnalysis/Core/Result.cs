using System;
using System.Collections.Generic;
using System.Linq;

namespace StrongHeart.TestTools.ComponentAnalysis.Core
{
    public class Result<T>
    {
        public IList<T> AllVerifiedItems { get; }
        public IList<T> ItemsWithError { get; }

        public bool IsPassed => ItemsWithError.Count == 0 && AllVerifiedItems.Count > 0;
        public string Message { get; }
        public string? Output { get; }

        private Result(IList<T> allVerifiedItems, IList<T> itemsWithError, string message, string? output)
        {
            AllVerifiedItems = allVerifiedItems;
            ItemsWithError = itemsWithError;
            Message = message;
            Output = output;
        }

        public static Result<T> NoItemsToVerify(bool throwOnException)
        {
            Result<T> result = new Result<T>(new T[0], new T[0], "No items found for verification. Check your selection.", null);
            if (throwOnException)
            {
                throw new RuleNotCompliedException<T>(result);
            }
            return result;
        }

        public static Result<T> NoErrors(IList<T> allVerifiedItems)
        {
            return new Result<T>(allVerifiedItems, new T[0], "All verified items complies to rule", null);
        }

        public static Result<T> ErrorsFound(IList<T> allVerifiedItems, IList<T> itemsWithError, string correctiveAction, bool throwOnException, string output)
        {
            Result<T> result = new Result<T>(allVerifiedItems, itemsWithError, correctiveAction, output);
            if (throwOnException)
            {
                throw new RuleNotCompliedException<T>(result);
            }
            return result;
        }

        public override string ToString()
        {
            return
                $@"Types verified: {AllVerifiedItems.Count}
Types with error {ItemsWithError.Count}:
{string.Join(Environment.NewLine, ItemsWithError.Select(x => "-" + x.ToString()))}
Details: {Output}";
        }
    }
}