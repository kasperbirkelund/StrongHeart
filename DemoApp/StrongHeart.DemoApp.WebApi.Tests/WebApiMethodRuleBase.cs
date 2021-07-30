using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using StrongHeart.TestTools.ComponentAnalysis.Core;

namespace StrongHeart.DemoApp.WebApi.Tests
{
    public abstract class WebApiMethodRuleBase : IRule<Type>
    {
        public abstract string CorrectiveAction { get; }

        public bool DoVerifyItem(Type item)
        {
            return item.GetCustomAttributes<ApiControllerAttribute>().Any();
        }

        public bool IsValid(Type item, Action<string> output)
        {
            string[] faultedMethods = item
                .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                .Where(MethodPredicate)
                .Select(x => $"{item.Name}.{x.Name}")
                .ToArray();

            Array.ForEach(faultedMethods, output);
            return faultedMethods.Length == 0;
        }

        protected abstract bool MethodPredicate(MethodInfo method);
    }
}