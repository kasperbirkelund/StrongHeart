using System.Reflection;
using StrongHeart.TestTools.ComponentAnalysis.Core;
using Xunit;

namespace StrongHeart.DemoApp.WebApi.Tests
{
    public class WebApiConventionsTest
    {
        private static readonly Assembly WebApiAssembly = typeof(Startup).Assembly;

        [Fact]
        public void WebApiMustHaveHttpMethodAttribute()
        {
            VerifyThat
                .AllTypesFromAssembly(WebApiAssembly)
                .DoesComplyToRule(new WebApiMustHaveHttpMethodAttributeRule());
        }
    }
}