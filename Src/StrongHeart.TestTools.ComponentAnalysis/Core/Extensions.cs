using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StrongHeart.TestTools.ComponentAnalysis.Core
{
    public static class Extensions
    {
        public static VerificationResult<T> DoesComplyToRule<T>(this IEnumerable<T> items, IRule<T> rule, bool throwOnException = true)
        {
            IList<T> allItems = items.ToArray();

            T[] typesToVerify = allItems
                .Where(rule.DoVerifyItem)
                .ToArray();

            if (typesToVerify.Length == 0 /*&& rule.DoFailIfNoItemsToVerify*/)
            {
                return VerificationResult<T>.NoItemsToVerify(throwOnException);
            }

            StringBuilder sb = new ();

            T[] itemsWithError = typesToVerify
                .Where(x => !rule.IsValid(x, s => sb.AppendLine(s)))
                .ToArray();

            if (itemsWithError.Any())
            {
                return VerificationResult<T>.ErrorsFound(typesToVerify, itemsWithError, rule.CorrectiveAction, throwOnException, sb.ToString());
            }
            return VerificationResult<T>.NoErrors(typesToVerify);
        }

        internal static bool IsImmutable(this PropertyInfo property)
        {
            return property.IsInitOnly() || !property.CanWrite;
        }

        private static bool IsInitOnly(this PropertyInfo property)
        {
            if (!property.CanWrite)
            {
                return false;
            }

            MethodInfo? setMethod = property.SetMethod;
            Type[]? setMethodReturnParameterModifiers = setMethod?.ReturnParameter.GetRequiredCustomModifiers();
            if (setMethodReturnParameterModifiers == null)
            {
                return false;
            }
            return setMethodReturnParameterModifiers.Contains(typeof(System.Runtime.CompilerServices.IsExternalInit));
        }
    }
}