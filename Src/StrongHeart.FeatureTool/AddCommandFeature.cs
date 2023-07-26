using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

public class AddCommandFeature : AsyncCommand<AddFeatureSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, AddFeatureSettings settings)
    {
        string content = GetCommand(settings);
        await Helper.WriteFileAsync(settings, "Commands", content);
        return 0;
    }

    private static string GetCommand(AddFeatureSettings settings)
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

        return s;
    }
}