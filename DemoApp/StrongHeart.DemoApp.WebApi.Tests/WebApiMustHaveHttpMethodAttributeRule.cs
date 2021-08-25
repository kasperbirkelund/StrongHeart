using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Routing;

namespace StrongHeart.DemoApp.WebApi.Tests
{
    public class WebApiMustHaveHttpMethodAttributeRule : WebApiMethodRuleBase
    {
        public override string CorrectiveAction => "Add a HttpMethodAttribute to all public methods on api controllers";
        protected override bool IsInValidMethod(MethodInfo method)
        {
            if (IsGetter(method) || IsSetter(method))
            {
                return false;
            }
            return !method.GetCustomAttributes<HttpMethodAttribute>().Any();
        }
    }
}