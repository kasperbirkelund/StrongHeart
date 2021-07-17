using System.Collections.Generic;
using System.Reflection;
using StrongHeart.TestTools.ComponentAnalysis.Core;
using StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;
using Xunit;

namespace StrongHeart.TestTools.Xunit
{
    public abstract class FeatureConventionsTestBase
    {
        /// <summary>
        /// Lets the framework know where the Feature-containing assemblies are.
        /// </summary>
        protected abstract IEnumerable<Assembly> GetFeatureAssemblies();

        [Fact]
        public virtual void CommandFeaturesRequestAndResponseMatch()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new CommandFeaturesRequestAndResponseMatch());
        }

        [Fact]
        public virtual void FeaturesCannotDependOnFeaturesRule()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new FeaturesCannotDependOnFeaturesRule());
        }

        [Fact]
        public virtual void QueryFeaturesRequestAndResponseMatch()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new QueryFeaturesRequestAndResponseMatch());
        }

        [Fact]
        public virtual void ResponseDtoShouldBeMoreSpecificRule()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new ResponseDtoShouldBeMoreSpecificRule());
        }

        [Fact]
        public virtual void ListResponseResponseShouldBeImmutable()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new ListResponseResponseShouldBeImmutable());
        }

        [Fact]
        public virtual void SingleItemResponseShouldBeImmutable()
        {
            VerifyThat
                .AllTypesFromAssemblies(GetFeatureAssemblies())
                .DoesComplyToRule(new SingleItemResponseShouldBeImmutable());
        }
    }
}