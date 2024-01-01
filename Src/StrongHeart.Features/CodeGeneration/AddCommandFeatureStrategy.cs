namespace StrongHeart.Features.CodeGeneration;

internal class AddCommandFeatureStrategy : IGenerationStrategy
{
    public string Generate(AddFeatureSettings settings)
    {
        string s =
            $@"using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace {Helper.GetNamespace(settings, "Commands")}
{{
    [System.Diagnostics.DebuggerStepThrough]
    public record {settings.FeatureName}Dto() : IRequestDto;
    [System.Diagnostics.DebuggerStepThrough]
    public record {settings.FeatureName}Request(ICaller Caller, {settings.FeatureName}Dto Model) : IRequest<{settings.FeatureName}Dto>;
    
    public class {settings.FeatureName}Feature : {settings.FeatureBaseType}<{settings.FeatureName}Request, {settings.FeatureName}Dto>
    {{
        public override Task<Result> Execute({settings.FeatureName}Request request)
        {{
            throw new System.NotImplementedException();
        }}
    }}
}}";

        return new GeneratedCode(s, "");
    }
}