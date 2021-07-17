using System.Collections.Generic;
using System.Reflection;
using StrongHeart.TestTools.ComponentAnalysis.Core;
using StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;
using Xunit;
using Xunit.Abstractions;

namespace StrongHeart.TestTools.Xunit
{
    public abstract class FeatureConventionsTestBase
    {
        private readonly ITestOutputHelper _helper;

        /// <summary>
        /// Lets the framework know where the Feature-containing assemblies are.
        /// </summary>
        protected abstract IEnumerable<Assembly> GetFeatureAssemblies();

        protected FeatureConventionsTestBase(ITestOutputHelper helper)
        {
            _helper = helper;
        }

        [Fact]
        public virtual void CommandFeaturesRequestAndResponseMatch()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new CommandFeaturesRequestAndResponseMatch())
                .Print(s => _helper.WriteLine(s));
        }

        [Fact]
        public virtual void FeaturesCannotDependOnFeaturesRule()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new FeaturesCannotDependOnFeaturesRule())
                .Print(s => _helper.WriteLine(s)); 
        }

        [Fact]
        public virtual void QueryFeaturesRequestAndResponseMatch()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new QueryFeaturesRequestAndResponseMatch())
                .Print(s => _helper.WriteLine(s));
        }

        [Fact]
        public virtual void ResponseDtoShouldBeMoreSpecificRule()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new ResponseDtoShouldBeMoreSpecificRule())
                .Print(s => _helper.WriteLine(s));
        }

        [Fact]
        public virtual void ListResponseResponseShouldBeImmutable()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new ListResponseResponseShouldBeImmutable())
                .Print(s => _helper.WriteLine(s));
        }

        [Fact]
        public virtual void SingleItemResponseShouldBeImmutable()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new SingleItemResponseShouldBeImmutable())
                .Print(s => _helper.WriteLine(s));
        }
    }
}