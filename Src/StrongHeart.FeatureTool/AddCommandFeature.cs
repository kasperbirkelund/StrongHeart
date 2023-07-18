using System;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

public class AddCommandFeature : AsyncCommand<AddFeatureSettings>
{
    //public class AddCommandSettings : AddFeatureSettings
    //{
    //    //[CommandArgument(0, "<PACKAGE_NAME>")]
    //    //public string PackageName { get; set; }

    //    //[CommandOption("-v|--version <VERSION>")]
    //    //public string Version { get; set; }
    //}

    public override async Task<int> ExecuteAsync(CommandContext context, AddFeatureSettings settings)
    {
        await Console.Out.WriteLineAsync(GetCommand(settings.ProjectName, settings.FeatureName));
        return 0;
    }

    private static string GetCommand(string project, string commandName)
    {
        string s =
            $@"using System.Threading.Tasks;
using StrongHeart.Core.Security;
using StrongHeart.Features.Core;

namespace {project}.Features.Commands.{commandName}
{{
    {Helper.GetGeneratedCodeText()}
    public record {commandName}Dto() : IRequestDto;
    {Helper.GetGeneratedCodeText()}
    public record {commandName}Request(ICaller Caller, {commandName}Dto Model) : IRequest<{commandName}Dto>;
    
    {Helper.GetGeneratedCodeText()}
    public partial class {commandName}Feature : ICommandFeature<{commandName}Request, {commandName}Dto>
    {{
        public Task<Result> Execute({commandName}Request request)
        {{
            throw new System.NotImplementedException();
        }}
    }}
}}";

        return s;
    }
}