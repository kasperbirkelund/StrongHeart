using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StrongHeart.TestTools.ComponentAnalysis.Core
{
    public static class Extensions
    {
        public static Result<T> DoesComplyToRule<T>(this IEnumerable<T> items, IRule<T> rule, bool throwOnException = true)
        {
            IList<T> allItems = items.ToArray();

            T[] typesToVerify = allItems
                .Where(rule.DoVerifyItem)
                .ToArray();

            if (typesToVerify.Length == 0 && rule.DoFailIfNoItemsToVerify)
            {
                return Result<T>.NoItemsToVerify(throwOnException);
            }

            StringBuilder sb = new StringBuilder();

            T[] itemsWithError = typesToVerify
                .Where(x => !rule.IsValid(x, s => sb.AppendLine(s)))
                .ToArray();

            if (itemsWithError.Any())
            {
                return Result<T>.ErrorsFound(typesToVerify, itemsWithError, rule.CorrectiveAction, throwOnException, sb.ToString());
            }
            return Result<T>.NoErrors(typesToVerify);
        }

        public static TAttribute? GetAttributeOrNull<TAttribute>(this ICustomAttributeProvider provider)
        {
            return provider
                .GetCustomAttributes(typeof(TAttribute), false)
                .Cast<TAttribute>()
                .SingleOrDefault();
        }

        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider provider)
        {
            return provider.GetAttributeOrNull<TAttribute>() != null;
        }
    }
}