using StrongHeart.TestTools.ComponentAnalysis.Core;
using StrongHeart.TestTools.ComponentAnalysis.Core.DefaultRules;

namespace StrongHeart.TestTools.ComponentAnalysis;

public partial class ConventionsTestBase
{
    public virtual void CommandFeaturesRequestAndResponseMatch()
    {
        VerifyThat
            .AllTypesFromAssemblies(GetFeatureAssemblies())
            .DoesComplyToRule(new CommandFeaturesRequestAndResponseMatch())
            .Print(output);
    }

    public virtual void FeaturesCannotDependOnFeaturesRule()
    {
        VerifyThat
            .AllTypesFromAssemblies(GetFeatureAssemblies())
            .DoesComplyToRule(new FeaturesCannotDependOnFeaturesRule())
            .Print(output); 
    }

    public virtual void FeaturesMustOnlyHaveOneConstructor()
    {
        VerifyThat
            .AllTypesFromAssemblies(GetFeatureAssemblies())
            .DoesComplyToRule(new FeaturesMustOnlyHaveOneConstructor())
            .Print(output);
    }

    public virtual void QueryFeaturesRequestAndResponseMatch()
    {
        var result = VerifyThat
            .AllTypesFromAssemblies(GetFeatureAssemblies())
            .DoesComplyToRule(new QueryFeaturesRequestAndResponseMatch())
            .Print(output);

        //below part is not intended to be used in regular test scenarios. Just for demonstration.
        //Assert.True(result.IsPassed);
        //Assert.True(result.AllVerifiedItems.Count> 0);
        //result.AllVerifiedItems.Count.Should().BeGreaterThan(0);
        //result.ItemsWithError.Count.Should().Be(0);
        //result.Message.Should().Be("All verified items comply to rule");
        //result.Output.Should().BeNull();
    }

    public virtual void ResponseDtoShouldBeMoreSpecificRule()
    {
        VerifyThat
            .AllTypesFromAssemblies(GetFeatureAssemblies())
            .DoesComplyToRule(new ResponseDtoShouldBeMoreSpecificRule())
            .Print(output);
    }

    public virtual void ListResponseResponseShouldBeImmutable()
    {
        VerifyThat
            .AllTypesFromAssemblies(GetFeatureAssemblies())
            .DoesComplyToRule(new ListResponseResponseShouldBeImmutable())
            .Print(output);
    }

    public virtual void EventHandlersMustDependOnACommandFeature()
    {
        VerifyThat
            .AllTypesFromAssemblies(GetFeatureAssemblies())
            .DoesComplyToRule(new EventHandlersMustDependOnACommandFeatureRule())
            .Print(output);
    }

    public virtual void SingleItemResponseShouldBeImmutable()
    {
        VerifyThat
            .AllTypesFromAssemblies(GetFeatureAssemblies())
            .DoesComplyToRule(new SingleItemResponseShouldBeImmutable())
            .Print(output);
    }
}