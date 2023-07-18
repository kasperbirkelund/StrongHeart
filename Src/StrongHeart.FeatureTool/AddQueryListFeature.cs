using System;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace StrongHeart.FeatureTool;

public class AddQueryListFeature : AddQueryFeatureBase
{
    public override async Task<int> ExecuteAsync(CommandContext context, AddFeatureSettings settings)
    {
        await Console.Out.WriteLineAsync(GetQuery(settings.ProjectName, settings.FeatureName, true));
        return 0;
    }
}